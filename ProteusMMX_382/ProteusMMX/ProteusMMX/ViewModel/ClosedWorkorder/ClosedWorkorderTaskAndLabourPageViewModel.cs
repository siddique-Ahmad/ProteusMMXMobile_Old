using Newtonsoft.Json;
using ProteusMMX.Controls;
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
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
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

                    options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.CODE_39, ZXing.BarcodeFormat.CODE_93, ZXing.BarcodeFormat.CODE_128, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE };
                    options.TryHarder = false; options.BuildBarcodeReader().Options.AllowedLengths = new[] { 44 };
                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };
                    _scanner.AutoFocus();

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

                        //if (LabourEstimatedHours == "N")
                        //{

                        //}
                        //else
                        //{
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
                            Text = item.EstimatedHours,
                            
                        };

                        Taskgrid.Children.Add(EstHourseVal, 5, 0);
                        TaskStackLayout.Children.Add(Taskgrid);
                        //}
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
                            Label DescEntry = new Label()
                            {
                                HeightRequest = 40,
                                TextColor = Color.Black,
                                Text = item.Description,
                                FontSize = 13,
                                BackgroundColor=Color.LightGray,
                                Margin = new Thickness(0, 0, 0, 0)
                            };
                            DescStackLayout.Children.Add(DescEntry);
                        }
                        else
                        {
                            string result = RemoveHTML.StripHtmlTags(item.Description);

                            Label DescEntry = new Label()
                            {
                                HeightRequest=37,
                                TextColor = Color.Black,
                                Text = ShortString.short150(result),
                                FontSize = 13,
                                BackgroundColor = Color.LightGray,
                                Margin = new Thickness(0, 0, 0, 0)
                            };
                            DescStackLayout.Children.Add(DescEntry);
                        }

                        Label more = new Label
                        {
                            IsVisible= MoreTextIsEnable,
                            LineBreakMode=Xamarin.Forms.LineBreakMode.WordWrap,
                            HorizontalOptions = LayoutOptions.End,
                            TextColor = Color.FromHex("#006de0"),
                            Text = WebControlTitle.GetTargetNameByTitleName("More"),
                            FontAttributes = FontAttributes.Bold,
                            BackgroundColor = Color.LightGray,
                        };
                       
                        DescStackLayout.BackgroundColor = Color.LightGray;
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

                        #region ****** Hrs And Min ****
                        CustomEntry hoursEntry = new CustomEntry
                        {
                            Placeholder = "Hrs",
                            FontSize = 14,
                            WidthRequest = 45,
                            BackgroundColor = Color.LightGray
                        };

                        CustomEntry minuteEntry = new CustomEntry
                        {
                            Placeholder = "Min",
                            FontSize = 14,
                            WidthRequest = 45,
                            TextColor = Color.Black,
                            BackgroundColor = Color.LightGray
                        };

                        CustomEntry hoursEntryforRate2 = new CustomEntry
                        {
                            Placeholder = "Hrs",
                            FontSize = 14,
                            WidthRequest = 45,
                            TextColor = Color.Black,
                            BackgroundColor = Color.LightGray
                        };

                        CustomEntry minuteEntryforRate2 = new CustomEntry
                        {
                            Placeholder = "Min",
                            FontSize = 14,
                            WidthRequest = 45,
                            TextColor = Color.Black,                            
                            BackgroundColor = Color.LightGray
                        };
                        Label Hrs1 = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = "HourAtRate1"
                        };

                        Label Hrs12 = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = "HourAtRate2"
                        };
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
                        //CustomEntry hoursEntry ;
                        //CustomEntry minuteEntry;
                        //CustomEntry hoursEntryforRate2;
                        //CustomEntry minuteEntryforRate2;

                        Grid BMainGrid = new Grid();
                        BMainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        BMainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 45 });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        hrsMin.Children.Add(buttnoStackLayoutHrs, 0, 0);
                        buttnoStackLayoutHrs.Children.Add(BMainGrid);

                        
                        BMainGrid.Children.Add(Hrs1, 0, 0);
                        Grid.SetColumnSpan(Hrs1, 2);

                        StackLayout SpaceSl = new StackLayout();
                        BMainGrid.Children.Add(SpaceSl, 2, 0);
                        BMainGrid.Children.Add(Hrs12, 3, 0);
                        Grid.SetColumnSpan(Hrs12, 2);

                        #region ***** Start Button ****

                        //StackLayout startButtonStackLayout = new StackLayout
                        //{
                        //    Margin = new Thickness(-6, 0, 0, 0)
                        //};
                        //BMainGrid.Children.Add(startButtonStackLayout, 0, 0);
                        //Grid startButtonGrid = new Grid();
                        //startButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        //SfButton startButton = new SfButton
                        //{
                        //    Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                        //    FontAttributes = FontAttributes.Bold,
                        //    ShowIcon = true,
                        //    BackgroundColor = Color.Transparent,
                        //    CommandParameter = item,
                        //    TextColor = Color.Black,
                        //    ImageSource = "starticon.png"
                        //};

                        //startButtonGrid.Children.Add(startButton, 0, 0);
                        //startButtonStackLayout.Children.Add(startButtonGrid);



                        //#endregion

                        //#region ***** Stop Button ****

                        //StackLayout StopButtonStackLayout = new StackLayout
                        //{
                        //    Margin = new Thickness(-20, 0, 0, 0)
                        //};
                        //BMainGrid.Children.Add(StopButtonStackLayout, 1, 0);
                        //Grid StopButtonGrid = new Grid();
                        //StopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        //SfButton stopButton = new SfButton
                        //{
                        //    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                        //    FontAttributes = FontAttributes.Bold,
                        //    ShowIcon = true,
                        //    BackgroundColor = Color.Transparent,
                        //    TextColor = Color.Black,
                        //    CommandParameter = item,
                        //    ImageSource = "stopicon.png"
                        //};
                        //StopButtonGrid.Children.Add(stopButton, 0, 0);
                        //StopButtonStackLayout.Children.Add(StopButtonGrid);

                        //#endregion

                        //#region ***** complate Button ****

                        //StackLayout complateButtonStackLayout = new StackLayout
                        //{
                        //    Margin = new Thickness(-35, 0, 0, 0)
                        //};
                        //BMainGrid.Children.Add(complateButtonStackLayout, 2, 0);
                        //Grid complateButtonGrid = new Grid();
                        //complateButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        //SfButton completeButton = new SfButton
                        //{
                        //    Text = WebControlTitle.GetTargetNameByTitleName("Complete"),
                        //    FontAttributes = FontAttributes.Bold,
                        //    ShowIcon = true,
                        //    BackgroundColor = Color.Transparent,
                        //    TextColor = Color.Black,
                        //    CommandParameter = item,
                        //    ImageSource = "complateicon.png"
                        //};
                        //complateButtonGrid.Children.Add(completeButton, 0, 0);
                        //complateButtonStackLayout.Children.Add(complateButtonGrid);

                        #endregion

                        #region **** Hrs *****
                        StackLayout HrsStackLayout = new StackLayout();
                        BMainGrid.Children.Add(HrsStackLayout, 0, 1);
                        Grid HrsGrid = new Grid();
                        HrsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        HrsGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder HrsBorder = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,
                            IsEnabled = false,
                        };
                        HrsStackLayout.Children.Add(HrsGrid);
                        HrsGrid.Children.Add(HrsBorder, 0, 0);

                        HrsBorder.Content = hoursEntry;
                        #endregion

                        #region **** Min *****
                        StackLayout MinStackLayout = new StackLayout();
                        BMainGrid.Children.Add(MinStackLayout, 1, 1);
                        Grid MinGrid = new Grid();
                        MinGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        MinGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder MinBorder = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,
                            IsEnabled = false,
                        };
                        MinStackLayout.Children.Add(MinGrid);
                        MinGrid.Children.Add(MinBorder, 0, 0);

                        MinBorder.Content = minuteEntry;
                        #endregion
                        StackLayout SpaceSl1 = new StackLayout();
                        BMainGrid.Children.Add(SpaceSl1, 2, 1);
                        #region **** HrsforRate2 *****
                        StackLayout HrsStackLayoutforRate2 = new StackLayout();

                        BMainGrid.Children.Add(HrsStackLayoutforRate2, 3, 1);
                        Grid HrsGridforRate2 = new Grid();
                        HrsGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        HrsGridforRate2.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder HrsBorderforRate2 = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,
                            IsEnabled = false,
                        };

                        HrsStackLayoutforRate2.Children.Add(HrsGridforRate2);
                        HrsGridforRate2.Children.Add(HrsBorderforRate2, 0, 0);
                       
                        HrsBorderforRate2.Content = hoursEntryforRate2;
                        #endregion

                        #region **** Min Rate2*****
                        StackLayout MinStackLayoutforRate2 = new StackLayout();
                        BMainGrid.Children.Add(MinStackLayoutforRate2, 4, 1);
                        Grid MinGridforRate2 = new Grid();
                        MinGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        MinGridforRate2.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder MinBorderforRate2 = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,
                            IsEnabled = false,
                        };
                        MinStackLayoutforRate2.Children.Add(MinGridforRate2);
                        MinGridforRate2.Children.Add(MinBorderforRate2, 0, 0);
                        
                        MinBorderforRate2.Content = minuteEntryforRate2;
                        #endregion

                        if (AppSettings.User.EnableHoursAtRate == false)
                        {
                            buttnoStackLayoutHrs.IsVisible = false;
                        }
                        #endregion

                        #region ***** From Date And Button ******
                        var completeDateButton = new Button();
                        var FromDateButton = new Button();

                        StackLayout FromStackLayout = new StackLayout();
                        MainGrid.Children.Add(buttnoStackLayout, 0, 5);
                        Grid FromMainGrid = new Grid();
                        FromMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        FromMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        FromMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

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
                        
                        FromDateButton.BackgroundColor = Color.LightGray;
                        FromDateButton.BorderColor = Color.Black;
                        FromDateButton.BorderWidth = 1;
                        FromDateButton.CornerRadius = 10;
                        FromDateButton.TextColor = Color.Black;
                        FromDateButton.FontSize = 13;
                        FromDateButton.HeightRequest = 35;
                        FromDateButton.Margin = new Thickness(0, 0, 0, 0);
                        FromDateStackLayout.IsEnabled = false;
                        FromDateStackLayout.Children.Add(FromDateButton);

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
                       
                        completeDateButton.BackgroundColor = Color.LightGray;
                        completeDateButton.BorderColor = Color.Black;
                        completeDateButton.BorderWidth = 1;
                        completeDateButton.CornerRadius = 10;
                        completeDateButton.TextColor = Color.Black;
                        completeDateButton.FontSize = 13;
                        completeDateButton.HeightRequest = 35;
                        completeDateButton.Margin = new Thickness(0, 0, 0, 0);
                        CmpDateStackLayout.IsEnabled = false;
                        CmpDateStackLayout.Children.Add(completeDateButton);
                        // CsfBorder.Content = completeDateButton;
                        #endregion

                        #region ****** Save Button *****
                        if (AppSettings.User.EnableHoursAtRate == false)
                        {
                            StackLayout SbtnStackLayout = new StackLayout();
                            FromMainGrid.Children.Add(SbtnStackLayout, 2, 0);

                           
                            SbtnStackLayout.Children.Add(Hrs1);

                            Grid CmpHrsMin = new Grid();
                            CmpHrsMin.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            CmpHrsMin.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            SbtnStackLayout.Children.Add(CmpHrsMin);

                            #region **** Hrs *****
                            StackLayout HrsStackLayout1 = new StackLayout();
                            CmpHrsMin.Children.Add(HrsStackLayout1, 0, 0);
                            Grid HrsGrid1 = new Grid();
                            HrsGrid1.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            HrsGrid1.RowDefinitions.Add(new RowDefinition { Height = 40 });
                            SfBorder HrsBorderq = new SfBorder
                            {
                                BorderColor = Color.Black,
                                CornerRadius = 10,
                            };
                            HrsStackLayout1.Children.Add(HrsGrid1);
                            HrsGrid1.Children.Add(HrsBorderq, 0, 0);
                           
                            HrsBorderq.Content = hoursEntry;
                            #endregion

                            #region **** Min *****
                            StackLayout MinStackLayout1 = new StackLayout();
                            CmpHrsMin.Children.Add(MinStackLayout1, 1, 0);
                            Grid MinGrid1 = new Grid();
                            MinGrid1.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            MinGrid1.RowDefinitions.Add(new RowDefinition { Height = 40 });
                            SfBorder MinBorder1 = new SfBorder
                            {
                                BorderColor = Color.Black,
                                CornerRadius = 10,
                            };
                            MinStackLayout1.Children.Add(MinGrid1);
                            MinGrid1.Children.Add(MinBorder1, 0, 0);
                          
                            MinBorder1.Content = minuteEntry;
                            #endregion
                        }

                        #endregion

                        #region **** Hrs and min ****
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(item.HoursAtRate1))
                            {
                                string FinalHours = item.HoursAtRate1.ToString();
                                var FinalHrs1 = FinalHours.Split('.');
                                hoursEntry.Text = FinalHrs1[0];
                                minuteEntry.Text = FinalHrs1[1];
                            }
                            else
                            {

                                hoursEntry.Text = "0";
                                minuteEntry.Text = "0";
                            }

                            // string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate2)).ToString();
                            if (!string.IsNullOrWhiteSpace(item.HoursAtRate2))
                            {
                                string FinalHours = item.HoursAtRate2.ToString();
                                var FinalHrs2 = FinalHours.Split('.');
                                hoursEntryforRate2.Text = FinalHrs2[0];
                                minuteEntryforRate2.Text = FinalHrs2[1];
                            }
                            else
                            {

                                hoursEntryforRate2.Text = "0";
                                minuteEntryforRate2.Text = "0";
                            }

                            if (item.CompletionDate != null)
                            {
                                completeDateButton.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString();
                            }
                            if (item.StartDate != null)
                            {
                                FromDateButton.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString();
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

                        #endregion

                        contentLayout.Children.Add(MasterstackLayout);
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
                var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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
                if (Application.Current.Properties.ContainsKey("TaskOrInspection"))
                {
                    string TaskorInspection = (string)Application.Current.Properties["TaskOrInspection"];
                    if (TaskorInspection == "Inspections")
                    {
                        DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                        DisabledTextIsEnable = true;
                        return;
                    }
                }

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
