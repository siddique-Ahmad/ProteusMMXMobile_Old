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
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.Expander;
using Syncfusion.XForms.TabView;
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
        //StackLayout layout1 = new StackLayout();
        StackLayout FinalLayout = new StackLayout();
        private readonly IRequestService _requestService;
        Label TotalInspectionTimeHours;
        Label TotalInspectionTimeMinutes;
        TimeSpan total;
        Label TotalInspectionTime;
        // Button btnCreateWorkorder;
        StackLayout CreateWorkorderButtonSL;
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

        public string InspectionRRight { get; set; }
        public string EmployeeContratorRRight { get; set; }
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
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

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
            total = TimeSpan.Zero;
            // for disscoson 
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            AnswerText.Clear();

            if (Application.Current.Properties.ContainsKey("RemoveInspection"))
            {
                var InspectionRRight = Application.Current.Properties["RemoveInspection"].ToString();
                this.InspectionRRight = InspectionRRight;
            }

            if (Application.Current.Properties.ContainsKey("RemoveEmployeeContrator"))
            {
                var EmployeeContratorRRight = Application.Current.Properties["RemoveEmployeeContrator"].ToString();
                this.EmployeeContratorRRight = EmployeeContratorRRight;
            }

            
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
            UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
            await Task.Delay(3000);
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
            //InspectionButtonSL.Children.Clear();
            //TotalInspectionSL.Children.Clear();
            //masterGrid.Children.Clear();
            #region **** Main Grid ****

            // InitializeComponent();
            Grid masterGrid = new Grid();
            masterGrid.BackgroundColor = Color.White;
            masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            //masterGrid.RowDefinitions.Add(new RowDefinition { Height = 5 });
            //masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            //masterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            masterGrid.Children.Clear();
            #region ******TotalInspection******
            StackLayout TotalInspectionSL = new StackLayout();
            TotalInspectionSL.Padding = new Thickness(5, 2, 0, 0);
            masterGrid.Children.Add(TotalInspectionSL, 0, 0);


            TotalInspectionTime = new Label();
            TotalInspectionTimeHours = new Label();
            TotalInspectionTimeMinutes = new Label();

            #endregion

            #region ****** InspectionButton ******

            StackLayout InspectionButtonSL = new StackLayout();
            masterGrid.Children.Add(InspectionButtonSL, 0, 1);
            Grid InspectionButtonGrid = new Grid();
            InspectionButtonGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            InspectionButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            InspectionButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            InspectionButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            InspectionButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            InspectionButtonSL.Children.Add(InspectionButtonGrid);

            #region ****** AddInspectionButton ******

            StackLayout AddInspectionButtonSL = new StackLayout();
            AddInspectionButtonSL.IsVisible = false;
            InspectionButtonGrid.Children.Add(AddInspectionButtonSL, 0, 0);

            if (InspectionRights == "E" || InspectionRights == "V")
            {

                AddInspectionButtonSL.IsVisible = true;
            }

            Image InspectionButtonImage = new Image
            {
                Source = (Device.RuntimePlatform == Device.UWP) ? "Assets/Inspection1.png" : "Inspection1.png" ,
                HeightRequest = 25
            };

            Label InspectionButtonLal = new Label
            {
                Text = WebControlTitle.GetTargetNameByTitleName("AddInspection"),
                FontSize = 14.75,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, -7, 0, 0)
            };
            AddInspectionButtonSL.Children.Add(InspectionButtonImage);
            AddInspectionButtonSL.Children.Add(InspectionButtonLal);

            var btnAddInspection = new TapGestureRecognizer();
            AddInspectionButtonSL.GestureRecognizers.Add(btnAddInspection);
            btnAddInspection.Tapped += (sender, e) =>
            {
                InspectionButtonLal.TextColor = Color.FromHex("#000cfe");
                InspectionButtonImage.Source = "Inspection12.png";
                var page = new AddInspectionData(WorkorderID);
                Navigation.PushAsync(page);

            };
            #endregion

            #region ****** AddEmployeeButton ******

            StackLayout AddEmployeeButtonSL = new StackLayout();
            AddEmployeeButtonSL.IsVisible = false;
            InspectionButtonGrid.Children.Add(AddEmployeeButtonSL, 1, 0);

            Image EmployeeButtonImage = new Image
            {
                Source = (Device.RuntimePlatform == Device.UWP) ? "Assets/Inspection2.png" : "Inspection2.png" ,
                HeightRequest = 25
            };

            Label EmployeeButtonLal = new Label
            {
                Text = WebControlTitle.GetTargetNameByTitleName("AddEmployee"),
                FontSize = 14.75,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, -7, 0, 0)
            };
            AddEmployeeButtonSL.Children.Add(EmployeeButtonImage);
            AddEmployeeButtonSL.Children.Add(EmployeeButtonLal);

            var btnAddEmployee = new TapGestureRecognizer();
            AddEmployeeButtonSL.GestureRecognizers.Add(btnAddEmployee);
            btnAddEmployee.Tapped += (sender, e) =>
            {
                EmployeeButtonLal.TextColor = Color.FromHex("#000cfe");
                EmployeeButtonImage.Source = "Inspection22.png";
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "Inspection";
                ViewModel._navigationService.NavigateToAsync<EmployeeListSelectionPageViewModel>(tnobj);

            };
            #endregion

            #region ****** AddContractorButton ******

            StackLayout AddContractorButtonSL = new StackLayout();
            AddContractorButtonSL.IsVisible = false;
            InspectionButtonGrid.Children.Add(AddContractorButtonSL, 2, 0);
            Image ContractorButtonImage = new Image
            {
                Source = (Device.RuntimePlatform == Device.UWP) ? "Assets/Inspection3.png" : "Inspection3.png",
                HeightRequest = 25
            };

            Label ContractorButtonLal = new Label
            {
                Text = WebControlTitle.GetTargetNameByTitleName("AddContractor"),
                FontSize = 14.75,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, -7, 0, 0)
            };
            AddContractorButtonSL.Children.Add(ContractorButtonImage);
            AddContractorButtonSL.Children.Add(ContractorButtonLal);

            if (EmployeecontrcatorRights == "E" || EmployeecontrcatorRights == "V")
            {
                AddContractorButtonSL.IsVisible = true;
                AddEmployeeButtonSL.IsVisible = true;
            }

            var btnAddContractor = new TapGestureRecognizer();
            AddContractorButtonSL.GestureRecognizers.Add(btnAddContractor);
            btnAddContractor.Tapped += (sender, e) =>
            {
                ContractorButtonLal.TextColor = Color.FromHex("#000cfe");
                ContractorButtonImage.Source = "Inspection32.png";
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "Inspection";
                ViewModel._navigationService.NavigateToAsync<ContractorListSelectionPageViewModel>(tnobj);

            };
            #endregion

            #region ****** CreateWorkorderButton ******

            CreateWorkorderButtonSL = new StackLayout();
            CreateWorkorderButtonSL.IsVisible = false;
            InspectionButtonGrid.Children.Add(CreateWorkorderButtonSL, 3, 0);
            Image WorkorderButtonImage = new Image
            {
                Source = (Device.RuntimePlatform == Device.UWP) ? "Assets/workorder1.png" : "workorder1.png" ,
                HeightRequest = 25
            };

            Label WorkorderButtonLal = new Label
            {
                Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                FontSize = 14.75,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, -7, 0, 0)
            };
            CreateWorkorderButtonSL.Children.Add(WorkorderButtonImage);
            CreateWorkorderButtonSL.Children.Add(WorkorderButtonLal);

            if (CreateWorkorderButtonSL.IsVisible == true)
            {
                CreateWorkorderButtonSL.IsVisible = true;
            }

            var btnCreateWorkorder = new TapGestureRecognizer();
            CreateWorkorderButtonSL.GestureRecognizers.Add(btnCreateWorkorder);
            btnCreateWorkorder.Tapped += (sender, e) =>
            {
                WorkorderButtonLal.TextColor = Color.FromHex("#000cfe");
                WorkorderButtonImage.Source = "workorder2.png";
                var page = new CreateWorkorderFromInspectionPageContent(WorkorderID, AnswerText);
                Navigation.PushAsync(page);
            };
            #endregion
            
            CreateWorkorderButtonSL.IsVisible = false;

            //layout1.Children.Clear();
            FinalLayout.Children.Clear();
            MainLayout.Children.Clear();
            ParentLayout.Children.Clear();
            ParentLayout.Children.Remove(FinalLayout);

            await RetriveAllWorkorderInspectionsAsync();

            StackLayout FrameSL = new StackLayout
            {
                Padding = new Thickness(3, 25, 3, 0)
            };

            masterGrid.Children.Add(FrameSL, 0, 2);
            ScrollView FrameSV = new ScrollView();

            FrameSL.Children.Add(FrameSV);
            StackLayout FramesSL = new StackLayout();
            if (CC.listInspection.Count == 0)
            {
                FramesSL.IsEnabled = false;
            }
            FrameSV.Content = FramesSL;
            #endregion
            int CCCount = CC.workOrderEmployee.Count + CC.workorderContractor.Count;

            if (CCCount > 1)
            {
                MasterMainGrid.RowDefinitions[0].Height = GridLength.Star;
            }

            foreach (var item in CC.workOrderEmployee)
            {
                #region **** Frame content *********

                // FramesSL.Children.Add(AssociateLal);
                string workImpId = "";
                if (item.WorkOrderInspectionTimeID != null)
                {
                    workImpId = Convert.ToString(item.WorkOrderInspectionTimeID);
                }
                Frame Associateframe = new Frame
                {
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    StyleId = workImpId,
                };
               
                FramesSL.Children.Add(Associateframe);

                StackLayout AssociateSL = new StackLayout
                {
                    Margin = new Thickness(-15, -10, -15, -10)
                };
               
                Associateframe.Content = AssociateSL;
                #region **** Start Stop Hrs Min Delete Icon *****

                StackLayout StartButoonSL = new StackLayout();
                AssociateSL.Children.Add(StartButoonSL);

                Grid StartStopBtnGrid = new Grid();
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
                StartStopBtnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StartStopBtnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StartStopBtnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StartButoonSL.Children.Add(StartStopBtnGrid);

                StackLayout AssociateLayout = new StackLayout
                {
                    Margin = new Thickness(0, 0, 0, 0)
                };
                Grid AssociateGrid = new Grid();
                AssociateGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                AssociateGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                AssociateLayout.Children.Add(AssociateGrid);
                Label AssociateLal = new Label
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": " + item.EmployeeName + "   ",
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#006de0"),
                    Margin = new Thickness(0, 0, 0, 0),
                    StyleId = item.EmployeeLaborCraftID.ToString() + ":" + "EmployeeLaborCraft"
                };
                Label LaborCraft = new Label { Text = item.EmployeeLaborCraftID.ToString() + ":" + "EmployeeLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), };

                AssociateGrid.Children.Add(AssociateLal, 0, 0);
                AssociateGrid.Children.Add(LaborCraft, 0, 1);
                StartStopBtnGrid.Children.Add(AssociateLayout, 0, 0);

                Grid.SetColumnSpan(AssociateLayout, 5);

                #region **** Start button ****

                StackLayout StartBtnStackLayout = new StackLayout
                {
                    Margin = new Thickness(0, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(StartBtnStackLayout, 0, 1);

                Grid StartBtnGrid = new Grid();

                StartBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StartBtnStackLayout.Children.Add(StartBtnGrid);
                SfButton startButton = new SfButton
                {
                    FontSize = 12,
                    BorderWidth = 2,
                    WidthRequest = 85,
                    Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                    FontAttributes = FontAttributes.Bold,
                    ShowIcon = true,
                    BackgroundColor = Color.Transparent,
                    TextColor = Color.Black,
                    IsEnabled = true,
                    CommandParameter = item,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon.png" : "starticon.png",
                    

                };
                StartBtnGrid.Children.Add(startButton, 0, 0);

                #endregion
                #region **** Stop button ****

                StackLayout StopBtnStackLayout = new StackLayout
                {
                    Margin = new Thickness(-10, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(StopBtnStackLayout, 1, 1);

                Grid StopBtnGrid = new Grid();
                StopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StopBtnStackLayout.Children.Add(StopBtnGrid);
                string WorkOrderInspectionDetailId = null;
                if (item.WorkOrderInspectionDetailsID != null)
                {
                    WorkOrderInspectionDetailId = Convert.ToString(item.WorkOrderInspectionDetailsID);
                }
                SfButton stopButton = new SfButton
                {
                    FontSize = 12,
                    BorderWidth = 2,
                    WidthRequest = 85,
                    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                    FontAttributes = FontAttributes.Bold,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    IsEnabled = false,
                    TextColor = Color.Gray,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopcomplate.png" : "stopcomplate.png",
                    CommandParameter = item,
                    StyleId = WorkOrderInspectionDetailId,
                };
                StopBtnGrid.Children.Add(stopButton);

                #endregion
                #region **** Hrs ****

                StackLayout HrsStackLayout = new StackLayout
                {
                    Margin = new Thickness(-10, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(HrsStackLayout, 2, 1);

                Grid HrsGrid = new Grid();
                HrsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                HrsStackLayout.Children.Add(HrsGrid);
                SfBorder HrsBorder = new SfBorder
                {
                    HeightRequest = 40,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                HrsGrid.Children.Add(HrsBorder);
                Entry hoursEntry = new Entry
                {
                    HeightRequest = 40,
                    Placeholder = "hh",
                    FontSize = 12,
                    Margin = new Thickness(3, 0, 0, 0)
                };
                HrsBorder.Content = hoursEntry;
                #endregion
                #region **** Min ****

                StackLayout MinStackLayout = new StackLayout
                {
                    Margin = new Thickness(0, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(MinStackLayout, 3, 1);

                Grid MinGrid = new Grid();
                MinGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                MinStackLayout.Children.Add(MinGrid);
                SfBorder MinBorder = new SfBorder
                {
                    HeightRequest = 40,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                MinGrid.Children.Add(MinBorder);
                Entry minuteEntry = new Entry
                {
                    HeightRequest = 40,
                    Placeholder = "mm",
                    FontSize = 12,
                    Margin = new Thickness(3, 0, 0, 0)
                };
                MinBorder.Content = minuteEntry;

                hoursEntry.TextChanged += OnTextChanged1;
                minuteEntry.TextChanged += HoursTextChanged1;
                #endregion



                #region **** Delete Icon ****



                StackLayout DeleteStackLayout = new StackLayout
                {
                    Margin = new Thickness(-10, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(DeleteStackLayout, 4, 1);

                Grid DeleteGrid = new Grid();
                MinGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                DeleteStackLayout.Children.Add(DeleteGrid);
                SfButton btnDelete = new SfButton
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 25,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    StyleId = item.EmployeeLaborCraftID.ToString(),
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/delicon.png" : "delicon.png",
                };
                DeleteGrid.Children.Add(btnDelete);
                btnDelete.Clicked += BtnEmployeeDelete_Clicked;
                if (EmployeeContratorRRight == "N")
                {
                    btnDelete.IsVisible = false;
                }
                if (EmployeeContratorRRight == "V")
                {
                    btnDelete.IsEnabled = false;
                }
                #endregion

                #endregion

                #region ***** Date from And complate *****

                StackLayout DateButoonSL = new StackLayout();
                AssociateSL.Children.Add(DateButoonSL);
                Grid dateFromCompGrid = new Grid();
                dateFromCompGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                dateFromCompGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                DateButoonSL.Children.Add(dateFromCompGrid);

                StackLayout FromDateSL = new StackLayout();
                dateFromCompGrid.Children.Add(FromDateSL, 0, 0);
                Label FromDateLbl = new Label
                {
                    Text = "From Date",
                    FontSize = 13,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#333333")
                };
                FromDateSL.Children.Add(FromDateLbl);
                SfBorder dateFromBo = new SfBorder
                {
                    HeightRequest = 35,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                FromDateSL.Children.Add(dateFromBo);
                CustomDatePicker1 startDate;
                if (item.StartDate != null)
                {
                    startDate = new CustomDatePicker1
                    {
                        Margin = new Thickness(0, 5, 3, 0),
                        SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                    };
                    dateFromBo.Content = startDate;
                }
                else
                {
                    startDate = new CustomDatePicker1
                    {
                        Margin = new Thickness(0, 5, 3, 0),
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                    };
                    dateFromBo.Content = startDate;
                }

                StackLayout CompDateSL = new StackLayout();
                dateFromCompGrid.Children.Add(CompDateSL, 1, 0);
                Label CompDateLbl = new Label
                {
                    Text = "Completion Date",
                    FontSize = 13,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#333333")
                };
                CompDateSL.Children.Add(CompDateLbl);
                SfBorder dateCompBo = new SfBorder
                {
                    HeightRequest = 35,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                CompDateSL.Children.Add(dateCompBo);
                CustomDatePicker1 CompletionDate;
                if (item.CompletionDate != null)
                {
                    CompletionDate = new CustomDatePicker1
                    {
                        SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 3, 0),
                    };
                    dateCompBo.Content = CompletionDate;
                }

                else
                {
                    CompletionDate = new CustomDatePicker1
                    {
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 3, 0),
                    };
                    dateCompBo.Content = CompletionDate;
                }
                #endregion


                #region GlobalTimer Logic
                // WorkOrderEmployee savedemployeelocal = null;

                //try
                //{


                //    string k1 = "WorkOrderEmployee:" + item.WorkOrderInspectionTimeID;
                //    savedemployeelocal = JsonConvert.DeserializeObject<WorkOrderEmployee>(WorkorderInspectionStorge.Storage.Get(k1));



                //}
                //catch (Exception ex)
                //{

                //}

                //if (savedemployeelocal != null)
                //{
                //    try
                //    {
                //        //set in buttons commands

                //        startButton.CommandParameter = savedemployeelocal;
                //        stopButton.CommandParameter = savedemployeelocal;
                //        startDate.SelectedDate = savedemployeelocal.StartDate;
                //        CompletionDate.SelectedDate = savedemployeelocal.CompletionDate;
                //        if (savedemployeelocal.StartBtn == true)
                //        {
                //            startButton.TextColor = Color.Gray;
                //            startButton.ImageSource = "startcomplte.png";
                //            startButton.IsEnabled = false;
                //            startButton.BorderColor = Color.Transparent;

                //            stopButton.BorderColor = Color.Transparent;
                //            stopButton.IsEnabled = true;
                //            stopButton.TextColor = Color.Black;
                //            stopButton.ImageSource = "stopicon.png";

                //            hoursEntry.IsReadOnly = true;
                //            minuteEntry.IsReadOnly = true;
                //        }
                //        else
                //        {
                //            stopButton.TextColor = Color.Gray;
                //            stopButton.ImageSource = "stopcomplate.png";
                //            stopButton.IsEnabled = false;
                //            stopButton.BorderColor = Color.Transparent;

                //            startButton.TextColor = Color.Black;
                //            startButton.ImageSource = "starticon.png";
                //            startButton.IsEnabled = true;
                //            startButton.BorderColor = Color.Transparent;
                //            hoursEntry.IsReadOnly = false;
                //            minuteEntry.IsReadOnly = false;
                //        }


                //        //var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(savedemployeelocal.InspectionTime));
                //        //var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                //        //hoursEntry.Text = savedemployeelocal.Hours;
                //        //minuteEntry.Text = savedemployeelocal.Minutes;



                //    }
                //    catch (Exception ex)
                //    {

                //    }

                //}


                #endregion

                #region **** data base Button  working ****

              //  startDate.SelectedDate = item.StartDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(item.StartDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null;
                startDate.SelectedDate = item.StartDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone): (DateTime?)null;
                CompletionDate.SelectedDate = item.CompletionDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null;

                if (item.TimerStatus == "Start")
                {
                    startButton.TextColor = Color.Green;
                    startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon1.png" : "starticon1.png";
                    startButton.IsEnabled = false;
                    startButton.BorderColor = Color.Transparent;

                    stopButton.BorderColor = Color.Transparent;
                    stopButton.IsEnabled = true;
                    stopButton.TextColor = Color.Black;
                    stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopicon.png" : "stopicon.png";

                    hoursEntry.IsReadOnly = true;
                    minuteEntry.IsReadOnly = true;
                }
                else
                {
                    stopButton.TextColor = Color.Gray;
                    stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopcomplate.png" : "stopcomplate.png";
                    stopButton.IsEnabled = false;
                    stopButton.BorderColor = Color.Transparent;

                    startButton.TextColor = Color.Black;
                    startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopicon.png" : "stopicon.png";
                    startButton.IsEnabled = true;
                    startButton.BorderColor = Color.Transparent;
                    hoursEntry.IsReadOnly = false;
                    minuteEntry.IsReadOnly = false;
                }
                #endregion

                startButton.Clicked += async (sender, e) =>
                {
                    //save its workOrderLabor in local storage so we can start timer when we come on this page then we can retrive it.
                    var buttonStart = sender as SfButton;
                    WorkOrderEmployee workorderemployee = buttonStart.CommandParameter as WorkOrderEmployee;
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    await Task.Delay(1000);
                    var AddEmpTimer = new InspectionTOAnswers
                    {
                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        WorkOrderInspectionDetailsID = null,
                        UserID = Convert.ToInt32(UserID),
                        IsManual = false,
                        InspectionTime = null,
                        InspectionStartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        InspectionEndDate = null,//need to pass start date
                        InspectionCompletionDate = null,
                        WorkOrderInspectionTimeID = workorderemployee.WorkOrderInspectionTimeID,
                        ModifiedUserName = AppSettings.User.UserName,
                        ModifiedTimestamp = null,
                        TimerStatus = "Start"
                    };
                    var response = await ViewModel._inspectionService.CreateInspectionTimeDetails(AddEmpTimer);

                    if (response != null && bool.Parse(response.servicestatus))
                    {
                        workorderemployee.StartTimeOfTimer = DateTime.Now;
                        workorderemployee.StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        startButton.CommandParameter = workorderemployee; //reassign to commandParameter.
                        //workorderemployee.StopBtn = false;
                        //workorderemployee.StartBtn = true;

                        var parent = buttonStart.Parent.Parent.Parent;
                        Grid parentGrid = parent as Grid;
                        StackLayout StopBtnSL = parentGrid.Children[2] as StackLayout;
                        Grid StopBtnGrid1 = StopBtnSL.Children[0] as Grid;

                        //  parentGrid.StyleId = item.HoursAtRate1.ToString();
                        SfButton btnStopLocal = StopBtnGrid1.Children[0] as SfButton;//Find the stopbutton from parent
                                                                                     //btnStopLocal.CommandParameter = workorderemployee; //reassign to
                        if (response.InspectionStartedDate != null)
                        {
                            startDate.SelectedDate = response.InspectionStartedDate;
                        }
                        else
                        {
                            startDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        }
                        startButton.TextColor = Color.Green;
                        startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon1.png" : "starticon1.png";
                        startButton.IsEnabled = false;
                        startButton.BorderColor = Color.Transparent;

                        stopButton.BorderColor = Color.Transparent;
                        stopButton.IsEnabled = true;
                        stopButton.TextColor = Color.Black;
                        stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopicon.png" : "stopicon.png";

                        if (response.WorkOrderInspectionDetailsID != null)
                        {
                            stopButton.StyleId = Convert.ToString(response.WorkOrderInspectionDetailsID);
                        }

                        hoursEntry.IsReadOnly = true;
                        minuteEntry.IsReadOnly = true;
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                };

                stopButton.Clicked += async (sender, e) =>
                {
                    var StopTime = DateTime.Now;

                    var x1 = sender as SfButton;

                    WorkOrderEmployee workOrderemp = x1.CommandParameter as WorkOrderEmployee;
                    workOrderInspectionTimeID.Add(workOrderemp.WorkOrderInspectionTimeID);
                    if (!string.IsNullOrEmpty(x1.StyleId))
                    {
                        workOrderemp.WorkOrderInspectionDetailsID = Convert.ToInt32(x1.StyleId);
                    }
                    OnAlertYesNoClicked(workOrderemp);
                    //stopButton.TextColor = Color.Gray;
                    //stopButton.ImageSource = "stopcomplate.png";
                    //stopButton.IsEnabled = false;
                    //stopButton.BorderColor = Color.Transparent;

                    //startButton.TextColor = Color.Black;
                    //startButton.ImageSource = "starticon.png";
                    //startButton.IsEnabled = true;
                    //startButton.BorderColor = Color.Transparent;
                    ////this.BackgroundColor = Color.White;

                    //hoursEntry.IsReadOnly = false;
                    //minuteEntry.IsReadOnly = false;
                };
                #endregion
                async void OnAlertYesNoClicked(WorkOrderEmployee workOrderemp)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    await Task.Delay(1000);
                    var result = await UserDialogs.Instance.ConfirmAsync(WebControlTitle.GetTargetNameByTitleName("Didyoufinishyourwork...?"), WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Yes"), WebControlTitle.GetTargetNameByTitleName("No"));
                    WorkOrderEmployee savedemployeelocal1 = null;


                    InspectionTOAnswers AddEmpTimer;
                    if (result == true)
                    {

                        AddEmpTimer = new InspectionTOAnswers
                        {
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            WorkOrderInspectionDetailsID = workOrderemp.WorkOrderInspectionDetailsID,
                            UserID = Convert.ToInt32(UserID),
                            IsManual = false,
                            InspectionTime = null,
                            InspectionStartDate = null,
                            InspectionEndDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),//need to pass start date
                            InspectionCompletionDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            WorkOrderInspectionTimeID = workOrderemp.WorkOrderInspectionTimeID,
                            ModifiedUserName = AppSettings.User.UserName,
                            ModifiedTimestamp = null,
                            TimerStatus = "Complete"
                        };

                    }
                    else
                    {
                        AddEmpTimer = new InspectionTOAnswers
                        {
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            WorkOrderInspectionDetailsID = workOrderemp.WorkOrderInspectionDetailsID,
                            UserID = Convert.ToInt32(UserID),
                            IsManual = false,
                            InspectionTime = null,
                            InspectionStartDate = null,
                            InspectionEndDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),//need to pass start date
                            InspectionCompletionDate = null,
                            WorkOrderInspectionTimeID = workOrderemp.WorkOrderInspectionTimeID,
                            ModifiedUserName = AppSettings.User.UserName,
                            ModifiedTimestamp = null,
                            TimerStatus = "Stop"
                        };
                    }
                    var response = await ViewModel._inspectionService.CreateInspectionTimeDetails(AddEmpTimer);

                    if (response != null && bool.Parse(response.servicestatus))
                    {
                        if (result == true)
                        {
                            CompletionDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        }
                        if (response.TotalInspectionHours != null)
                        {
                            var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(response.TotalInspectionHours));
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
                        stopButton.TextColor = Color.Gray;
                        stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopcomplate.png" : "stopcomplate.png";
                        stopButton.IsEnabled = false;
                        stopButton.BorderColor = Color.White;

                        startButton.TextColor = Color.Black;
                        startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopicon.png" : "stopicon.png";
                        startButton.IsEnabled = true;
                        startButton.BorderColor = Color.White;
                        //this.BackgroundColor = Color.White;

                        hoursEntry.IsReadOnly = false;
                        minuteEntry.IsReadOnly = false;
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
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
            }

            foreach (var item in CC.workorderContractor)
            {
                #region **** Frame content *********

                // FramesSL.Children.Add(AssociateLal);
                string workImpId = "";
                if (item.WorkOrderInspectionTimeID != null)
                {
                    workImpId = Convert.ToString(item.WorkOrderInspectionTimeID);
                }
                Frame Associateframe = new Frame
                {
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    StyleId = workImpId,
                };
                FramesSL.Children.Add(Associateframe);

                StackLayout AssociateSL = new StackLayout
                {
                    Margin = new Thickness(-15, -10, -15, -10)
                };
                Associateframe.Content = AssociateSL;
                #region **** Start Stop Hrs Min Delete Icon *****

                StackLayout StartButoonSL = new StackLayout();
                AssociateSL.Children.Add(StartButoonSL);

                Grid StartStopBtnGrid = new Grid();
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Star
                });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StartStopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
                StartStopBtnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StartStopBtnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StartStopBtnGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StartButoonSL.Children.Add(StartStopBtnGrid);


                StackLayout AssociateLayout = new StackLayout
                {
                    Margin = new Thickness(0, 0, 0, 0)
                };
                Grid AssociateGrid = new Grid();
                AssociateGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                AssociateGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                AssociateLayout.Children.Add(AssociateGrid);
                Label AssociateLal = new Label
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": " + item.ContractorName + "   ",
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#006de0"),
                    Margin = new Thickness(0, 0, 0, 0)
                };

                Label LaborCraft = new Label { Text = item.ContractorLaborCraftID.ToString() + ":" + "ContractorLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), };

                AssociateGrid.Children.Add(AssociateLal, 0, 0);
                AssociateGrid.Children.Add(LaborCraft, 0, 1);
                StartStopBtnGrid.Children.Add(AssociateLayout, 0, 0);

                Grid.SetColumnSpan(AssociateLayout, 5);

                #region **** Start button ****

                StackLayout StartBtnStackLayout = new StackLayout
                {
                    Margin = new Thickness(0, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(StartBtnStackLayout, 0, 1);

                Grid StartBtnGrid = new Grid();

                StartBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StartBtnStackLayout.Children.Add(StartBtnGrid);
                SfButton startButton = new SfButton
                {
                    FontSize = 12,
                    BorderWidth = 2,
                    WidthRequest = 85,
                    Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                    FontAttributes = FontAttributes.Bold,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    IsEnabled = true,
                    CommandParameter = item,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon.png" : "starticon.png",
                };
                StartBtnGrid.Children.Add(startButton, 0, 0);

                #endregion
                #region **** Stop button ****

                StackLayout StopBtnStackLayout = new StackLayout
                {
                    Margin = new Thickness(-10, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(StopBtnStackLayout, 1, 1);

                Grid StopBtnGrid = new Grid();
                StopBtnGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                StopBtnStackLayout.Children.Add(StopBtnGrid);

                string WorkOrderInspectionDetailId = null;
                if (item.WorkOrderInspectionDetailsID != null)
                {
                    WorkOrderInspectionDetailId = Convert.ToString(item.WorkOrderInspectionDetailsID);
                }

                SfButton stopButton = new SfButton
                {
                    FontSize = 12,
                    BorderWidth = 2,
                    WidthRequest = 85,
                    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                    FontAttributes = FontAttributes.Bold,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    TextColor = Color.Gray,
                    IsEnabled = false,
                    CommandParameter = item,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopcomplate.png" : "stopcomplate.png",
                    StyleId = WorkOrderInspectionDetailId,
                };
                StopBtnGrid.Children.Add(stopButton);

                #endregion
                #region **** Hrs ****

                StackLayout HrsStackLayout = new StackLayout
                {
                    Margin = new Thickness(-10, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(HrsStackLayout, 2, 1);

                Grid HrsGrid = new Grid();
                HrsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                HrsStackLayout.Children.Add(HrsGrid);
                SfBorder HrsBorder = new SfBorder
                {
                    HeightRequest = 40,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                HrsGrid.Children.Add(HrsBorder);
                Entry hoursEntry = new Entry
                {
                    HeightRequest = 40,
                    Placeholder = "hh",
                    FontSize = 12,
                    Margin = new Thickness(3, 0, 0, 0)
                };
                HrsBorder.Content = hoursEntry;
                #endregion
                #region **** Min ****

                StackLayout MinStackLayout = new StackLayout
                {
                    Margin = new Thickness(0, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(MinStackLayout, 3, 1);

                Grid MinGrid = new Grid();
                MinGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                MinStackLayout.Children.Add(MinGrid);
                SfBorder MinBorder = new SfBorder
                {
                    HeightRequest = 40,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10,

                };
                MinGrid.Children.Add(MinBorder);
                Entry minuteEntry = new Entry
                {
                    HeightRequest = 40,
                    Placeholder = "mm",
                    FontSize = 12,
                    Margin = new Thickness(3, 0, 0, 0)
                };
                MinBorder.Content = minuteEntry;

                hoursEntry.TextChanged += OnTextChanged1;
                minuteEntry.TextChanged += HoursTextChanged1;
                #endregion

                #region **** Delete Icon ****

                StackLayout DeleteStackLayout = new StackLayout
                {
                    Margin = new Thickness(-10, 0, 0, 0)
                };
                StartStopBtnGrid.Children.Add(DeleteStackLayout, 4, 1);

                Grid DeleteGrid = new Grid();
                MinGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                DeleteStackLayout.Children.Add(DeleteGrid);
                SfButton btnDelete = new SfButton
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 25,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/delicon.png" : "delicon.png",
                    StyleId = item.ContractorLaborCraftID.ToString(),
                };
                DeleteGrid.Children.Add(btnDelete);
                btnDelete.Clicked += BtnContractorDelete_Clicked;
                #endregion
                if (EmployeeContratorRRight == "N")
                {
                    btnDelete.IsVisible = false;
                }
                if (EmployeeContratorRRight == "V")
                {
                    btnDelete.IsEnabled = false;
                }
                #endregion

                #region ***** Date from And complate *****

                StackLayout DateButoonSL = new StackLayout();
                AssociateSL.Children.Add(DateButoonSL);
                Grid dateFromCompGrid = new Grid();
                dateFromCompGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                dateFromCompGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                DateButoonSL.Children.Add(dateFromCompGrid);

                StackLayout FromDateSL = new StackLayout();
                dateFromCompGrid.Children.Add(FromDateSL, 0, 0);
                Label FromDateLbl = new Label
                {
                    Text = "From Date",
                    FontSize = 13,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#333333")
                };
                FromDateSL.Children.Add(FromDateLbl);
                SfBorder dateFromBo = new SfBorder
                {
                    HeightRequest = 35,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                FromDateSL.Children.Add(dateFromBo);
                CustomDatePicker1 startDate;
                if (item.StartDate != null)
                {
                    startDate = new CustomDatePicker1
                    {
                        Margin = new Thickness(0, 5, 3, 0),
                        SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2
                    };
                    dateFromBo.Content = startDate;
                }
                else
                {
                    startDate = new CustomDatePicker1
                    {
                        Margin = new Thickness(0, 5, 3, 0),
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start
                    };
                    dateFromBo.Content = startDate;
                }

                StackLayout CompDateSL = new StackLayout();
                dateFromCompGrid.Children.Add(CompDateSL, 1, 0);
                Label CompDateLbl = new Label
                {
                    Text = "Completion Date",
                    FontSize = 13,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#333333")
                };
                CompDateSL.Children.Add(CompDateLbl);
                SfBorder dateCompBo = new SfBorder
                {
                    HeightRequest = 35,
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };
                CompDateSL.Children.Add(dateCompBo);
                CustomDatePicker1 CompletionDate;
                if (item.CompletionDate != null)
                {
                    CompletionDate = new CustomDatePicker1
                    {
                        SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 3, 0),
                    };
                    dateCompBo.Content = CompletionDate;
                }

                else
                {
                    CompletionDate = new CustomDatePicker1
                    {
                        MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 3, 0),
                    };
                    dateCompBo.Content = CompletionDate;
                }
                #endregion

                #region GlobalTimer Logic

                #region **** data base Button  working ****
                startDate.SelectedDate = item.StartDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null;
                CompletionDate.SelectedDate = item.CompletionDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null;

                //startDate.SelectedDate = item.StartDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(item.StartDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null;

                //CompletionDate.SelectedDate = item.CompletionDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(item.CompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null;

                if (item.TimerStatus == "Start")
                {
                    startButton.TextColor = Color.Green;
                    startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon1.png" : "starticon1.png";
                    startButton.IsEnabled = false;
                    startButton.BorderColor = Color.White;

                    stopButton.BorderColor = Color.White;
                    stopButton.IsEnabled = true;
                    stopButton.TextColor = Color.Black;
                    stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopicon.png" : "stopicon.png";

                    hoursEntry.IsReadOnly = true;
                    minuteEntry.IsReadOnly = true;
                }
                else
                {
                    stopButton.TextColor = Color.Gray;
                    stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopcomplate.png" : "stopcomplate.png";
                    stopButton.IsEnabled = false;
                    stopButton.BorderColor = Color.White;

                    startButton.TextColor = Color.Black;
                    startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon.png" : "starticon.png";
                    startButton.IsEnabled = true;
                    startButton.BorderColor = Color.White;
                    hoursEntry.IsReadOnly = false;
                    minuteEntry.IsReadOnly = false;
                }
                #endregion

                //WorkorderContractor savedContractorlocal = null;

                //try
                //{

                //    string k1 = "WorkorderContracator:" + item.WorkOrderInspectionTimeID;
                //    savedContractorlocal = JsonConvert.DeserializeObject<WorkorderContractor>(WorkorderInspectionStorge.Storage.Get(k1));


                //}
                //catch (Exception)
                //{

                //}

                //if (savedContractorlocal != null)
                //{
                //    try
                //    {
                //        //set in buttons commands
                //        startButton.CommandParameter = savedContractorlocal;
                //        stopButton.CommandParameter = savedContractorlocal;
                //        startDate.SelectedDate = savedContractorlocal.StartDate;
                //        CompletionDate.SelectedDate = savedContractorlocal.CompletionDate;
                //        if (savedContractorlocal.StartBtn == true)
                //        {
                //            startButton.TextColor = Color.Gray;
                //            startButton.ImageSource = "startcomplte.png";
                //            startButton.IsEnabled = false;
                //            startButton.BorderColor = Color.Transparent;

                //            stopButton.BorderColor = Color.Transparent;
                //            stopButton.IsEnabled = true;
                //            stopButton.TextColor = Color.Black;
                //            stopButton.ImageSource = "stopicon.png";

                //            hoursEntry.IsReadOnly = true;
                //            minuteEntry.IsReadOnly = true;
                //        }
                //        else
                //        {
                //            stopButton.TextColor = Color.Gray;
                //            stopButton.ImageSource = "stopcomplate.png";
                //            stopButton.IsEnabled = false;
                //            stopButton.BorderColor = Color.Transparent;

                //            startButton.TextColor = Color.Black;
                //            startButton.ImageSource = "starticon.png";
                //            startButton.IsEnabled = true;
                //            startButton.BorderColor = Color.Transparent;
                //            hoursEntry.IsReadOnly = false;
                //            minuteEntry.IsReadOnly = false;
                //        }


                //        //var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(savedContractorlocal.InspectionTime));
                //        //var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                //        hoursEntry.Text = savedContractorlocal.Hours;
                //        minuteEntry.Text = savedContractorlocal.Minutes;

                //    }
                //    catch (Exception ex)
                //    {

                //    }

                //}


                #endregion


                startButton.Clicked += async (sender, e) =>
                {
                    //save its workOrderLabor in local storage so we can start timer when we come on this page then we can retrive it.
                    var buttonStart = sender as SfButton;
                    WorkorderContractor workordercontrcator = buttonStart.CommandParameter as WorkorderContractor;
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    await Task.Delay(1000);
                    var AddEmpTimer = new InspectionTOAnswers
                    {
                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        WorkOrderInspectionDetailsID = null,
                        UserID = Convert.ToInt32(UserID),
                        IsManual = false,
                        InspectionTime = null,
                        InspectionStartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        InspectionEndDate = null,//need to pass start date
                        InspectionCompletionDate = null,
                        WorkOrderInspectionTimeID = workordercontrcator.WorkOrderInspectionTimeID,
                        ModifiedUserName = AppSettings.User.UserName,
                        ModifiedTimestamp = null,
                        TimerStatus = "Start"
                    };

                    var response = await ViewModel._inspectionService.CreateInspectionTimeDetails(AddEmpTimer);

                    if (response != null && bool.Parse(response.servicestatus))
                    {
                        workordercontrcator.StartTimeOfTimer = DateTime.Now;
                        workordercontrcator.StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        startButton.CommandParameter = workordercontrcator; //reassign to commandParameter.
                        //workorderemployee.StopBtn = false;
                        //workorderemployee.StartBtn = true;

                        var parent = buttonStart.Parent.Parent.Parent;
                        Grid parentGrid = parent as Grid;
                        StackLayout StopBtnSL = parentGrid.Children[2] as StackLayout;
                        Grid StopBtnGrid1 = StopBtnSL.Children[0] as Grid;

                        //  parentGrid.StyleId = item.HoursAtRate1.ToString();
                        SfButton btnStopLocal = StopBtnGrid1.Children[0] as SfButton;//Find the stopbutton from parent
                                                                                     //btnStopLocal.CommandParameter = workorderemployee; //reassign to
                        if (response.InspectionStartedDate != null)
                        {
                            startDate.SelectedDate = response.InspectionStartedDate;
                        }
                        else
                        {
                            startDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        }
                        startButton.TextColor = Color.Green;
                        startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon1.png" : "starticon1.png";
                        startButton.IsEnabled = false;
                        startButton.BorderColor = Color.Transparent;

                        stopButton.BorderColor = Color.Transparent;
                        stopButton.IsEnabled = true;
                        stopButton.TextColor = Color.Black;
                        stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopicon.png" : "stopicon.png";

                        if (response.WorkOrderInspectionDetailsID != null)
                        {
                            stopButton.StyleId = Convert.ToString(response.WorkOrderInspectionDetailsID);
                        }

                        hoursEntry.IsReadOnly = true;
                        minuteEntry.IsReadOnly = true;
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                    //workordercontrcator.StartTimeOfTimer = DateTime.Now;
                    //workordercontrcator.StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                    //startButton.CommandParameter = workordercontrcator; //reassign to commandParameter.
                    //workordercontrcator.StopBtn = false;
                    //workordercontrcator.StartBtn = true;

                    //var parent = buttonStart.Parent.Parent.Parent;
                    //Grid parentGrid = parent as Grid;
                    //StackLayout StopBtnSL = parentGrid.Children[2] as StackLayout;
                    //Grid StopBtnGrid1 = StopBtnSL.Children[0] as Grid;

                    ////  parentGrid.StyleId = item.HoursAtRate1.ToString();
                    //SfButton btnStopLocal = StopBtnGrid1.Children[0] as SfButton;//Find the stopbutton from parent
                    //btnStopLocal.CommandParameter = workordercontrcator; //reassign to commandParameter to stopbutton



                    ////Save in Local
                    //string key = "WorkorderContracator:" + workordercontrcator.WorkOrderInspectionTimeID;
                    //// workorderempcontrcator.Description = "";
                    //WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(workordercontrcator));
                    //startDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);


                    ////StartTime = DateTime.Now;

                    //startButton.TextColor = Color.Gray;
                    //startButton.ImageSource = "startcomplte.png";
                    //startButton.IsEnabled = false;
                    //startButton.BorderColor = Color.Transparent;

                    //stopButton.BorderColor = Color.Transparent;
                    //stopButton.IsEnabled = true;
                    //stopButton.TextColor = Color.Black;
                    //stopButton.ImageSource = "stopicon.png";

                    //hoursEntry.IsReadOnly = true;
                    //minuteEntry.IsReadOnly = true;
                };

                stopButton.Clicked += (sender, e) =>
                {
                    var StopTime = DateTime.Now;

                    var x1 = sender as SfButton;
                    WorkorderContractor workOrdercontrcator = x1.CommandParameter as WorkorderContractor;
                    workOrderInspectionTimeID.Add(workOrdercontrcator.WorkOrderInspectionTimeID);
                    if (!string.IsNullOrEmpty(x1.StyleId))
                    {
                        workOrdercontrcator.WorkOrderInspectionDetailsID = Convert.ToInt32(x1.StyleId);
                    }


                    OnAlertYesNoClicked(workOrdercontrcator);

                    //if (workOrdercontrcator.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                    //{
                    //    return;
                    //}

                    //TimeSpan elapsed = StopTime.Subtract(workOrdercontrcator.StartTimeOfTimer);

                    //int mn = elapsed.Minutes;
                    //if (String.IsNullOrWhiteSpace(minuteEntry.Text))
                    //{
                    //    minuteEntry.Text = "0";
                    //}
                    //if (String.IsNullOrWhiteSpace(hoursEntry.Text))
                    //{
                    //    hoursEntry.Text = "0";
                    //}
                    //int mn1 = Convert.ToInt32(minuteEntry.Text);
                    //if (mn + mn1 > 59)
                    //{


                    //    TimeSpan span = TimeSpan.FromMinutes(mn + mn1);
                    //    string elapsedTime1 = String.Format("{0:00}:{1:00}",
                    //                                  span.Hours, span.Minutes);
                    //    int hrs = span.Hours;
                    //    int hrs1 = Convert.ToInt32(hoursEntry.Text);
                    //    hoursEntry.Text = (hrs + hrs1).ToString();

                    //    int hrs2 = span.Minutes;
                    //    minuteEntry.Text = hrs2.ToString();
                    //}
                    //else
                    //{

                    //    int hrs = elapsed.Hours;
                    //    int hrs1 = Convert.ToInt32(hoursEntry.Text);
                    //    hoursEntry.Text = (hrs + hrs1).ToString();

                    //    int hrs2 = elapsed.Minutes;
                    //    int hrs21 = Convert.ToInt32(minuteEntry.Text);
                    //    minuteEntry.Text = (hrs2 + hrs21).ToString();
                    //}


                    //OnAlertYesNoClicked(minuteEntry.Text, hoursEntry.Text);
                    //stopButton.TextColor = Color.Gray;
                    //stopButton.ImageSource = "stopcomplate.png";
                    //stopButton.IsEnabled = false;
                    //stopButton.BorderColor = Color.Transparent;

                    //startButton.TextColor = Color.Black;
                    //startButton.ImageSource = "starticon.png";
                    //startButton.IsEnabled = true;
                    //startButton.BorderColor = Color.Transparent;
                    ////this.BackgroundColor = Color.White;

                    //hoursEntry.IsReadOnly = false;
                    //minuteEntry.IsReadOnly = false;

                };

                #endregion

                async void OnAlertYesNoClicked(WorkorderContractor workOrdercontrcator)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    await Task.Delay(1000);
                    var result = await UserDialogs.Instance.ConfirmAsync(WebControlTitle.GetTargetNameByTitleName("Didyoufinishyourwork...?"), WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Yes"), WebControlTitle.GetTargetNameByTitleName("No"));

                    InspectionTOAnswers AddEmpTimer;

                    if (result == true)
                    {

                        AddEmpTimer = new InspectionTOAnswers
                        {
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            WorkOrderInspectionDetailsID = workOrdercontrcator.WorkOrderInspectionDetailsID,
                            UserID = Convert.ToInt32(UserID),
                            IsManual = false,
                            InspectionTime = null,
                            InspectionStartDate = null,
                            InspectionEndDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),//need to pass start date
                            InspectionCompletionDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            WorkOrderInspectionTimeID = workOrdercontrcator.WorkOrderInspectionTimeID,
                            ModifiedUserName = AppSettings.User.UserName,
                            ModifiedTimestamp = null,
                            TimerStatus = "Complete"
                        };
                    }
                    else
                    {
                        AddEmpTimer = new InspectionTOAnswers
                        {
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            WorkOrderInspectionDetailsID = workOrdercontrcator.WorkOrderInspectionDetailsID,
                            UserID = Convert.ToInt32(UserID),
                            IsManual = false,
                            InspectionTime = null,
                            InspectionStartDate = null,
                            InspectionEndDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),//need to pass start date
                            InspectionCompletionDate = null,
                            WorkOrderInspectionTimeID = workOrdercontrcator.WorkOrderInspectionTimeID,
                            ModifiedUserName = AppSettings.User.UserName,
                            ModifiedTimestamp = null,
                            TimerStatus = "Stop"
                        };
                    }

                    var response = await ViewModel._inspectionService.CreateInspectionTimeDetails(AddEmpTimer);

                    if (response != null && bool.Parse(response.servicestatus))
                    {
                        if (result == true)
                        {
                            CompletionDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        }
                        if (response.TotalInspectionHours != null)
                        {
                            var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(response.TotalInspectionHours));
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
                        stopButton.TextColor = Color.Gray;
                        stopButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/stopcomplate.png" : "stopcomplate.png";
                        stopButton.IsEnabled = false;
                        stopButton.BorderColor = Color.Transparent;

                        startButton.TextColor = Color.Black;
                        startButton.ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/starticon.png" : "starticon.png";
                        startButton.IsEnabled = true;
                        startButton.BorderColor = Color.Transparent;
                        //this.BackgroundColor = Color.White;

                        hoursEntry.IsReadOnly = false;
                        minuteEntry.IsReadOnly = false;
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
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

            }

            //BoxView boxView = new BoxView
            //{
            //    BackgroundColor = Color.Black,
            //};
            //masterGrid.Children.Add(boxView, 0, 3);

           

            Label TotalInspectionLal = new Label
            {
                Text = WebControlTitle.GetTargetNameByTitleName("TotalInspectionTime") + "(hh:mm) " + ":  " + TotalInspectionTime.Text,
                FontSize = 14.75,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };
            TotalInspectionSL.Children.Add(TotalInspectionLal);

            ParentLayout.Children.Add(masterGrid);
            MainBoxView.Children.Clear();
            BoxView LineBoxView = new BoxView();
            LineBoxView.BackgroundColor = Color.Black;
            LineBoxView.HeightRequest = 2;
            LineBoxView.Margin = new Thickness(0, 0, 0, 0);

            MainBoxView.Children.Add(LineBoxView);
            #endregion
            if (this.CreateWorkorderButtonSL.IsVisible)
            {
                CC = await ViewModel._inspectionService.GetFailedWorkorderInspection(this.WorkorderID.ToString(), "1");

            }
            else
            {
                CC = await ViewModel._inspectionService.GetFailedWorkorderInspection(this.WorkorderID.ToString(), "0");
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
            try
            {

                CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
                if (CC.workOrderEmployee.Count == 0 && CC.listInspection.Count > 0)
                {
                    await DefaultAddEmploy();
                    CC = null;
                    CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
                }
                if (CC.workOrderEmployee.Count > 0 && CC.listInspection.Count > 0)
                {
                    BindLayout(CC.listInspection);
                }
                else if (CC.workOrderEmployee.Count == 0 && CC.listInspection.Count == 0)
                {
                    BindLayout(CC.listInspection);
                }
                else if (CC.workOrderEmployee.Count > 0 && CC.listInspection.Count == 0)
                {
                    BindLayout(CC.listInspection);
                }
            }
            catch (Exception)
            {

            }

        }

        private async Task DefaultAddEmploy()
        {
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

                //await RetriveAllWorkorderInspectionsAsync();
            }

        }

        //private void Timer_Clicked(object sender, EventArgs e)
        //{
        //    var btn = sender as Button;

        //    if (btn.Text == WebControlTitle.GetTargetNameByTitleName("StartTimer"))
        //    {
        //        btn.Text = WebControlTitle.GetTargetNameByTitleName("StopTimer");
        //        var context = btn.BindingContext as InspectionTimer;

        //        context.InspectionStartTime = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone); //DateTime.Now;

        //        btn.BackgroundColor = Color.Red;
        //        context.IsTimerRunning = true;

        //        #region Global Time Logic

        //        try
        //        {
        //            //Save in Local
        //            string key = "WorkOrderInspection:" + context.WorkorderID;
        //            WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(context));
        //        }
        //        catch (Exception ex)
        //        {


        //        }

        //        #endregion

        //    }

        //    else
        //    {
        //        btn.Text = WebControlTitle.GetTargetNameByTitleName("StartTimer");
        //        var context = btn.BindingContext as InspectionTimer;
        //        context.InspectionStopTime = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone); //DateTime.Now;

        //        var difference = context.InspectionStopTime - context.InspectionStartTime;
        //        context.TotalRunningTime = context.TotalRunningTime + difference.Value;
        //        // this.TimerText.Text = TimeSpan.FromSeconds(Convert.ToDouble((int)context.TotalRunningTime.TotalSeconds)).ToString();   //String.Format("Hours: {0} Minutes: {1} Seconds: {2}", context.TotalRunningTime.Hours, context.TotalRunningTime.Minutes, context.TotalRunningTime.Seconds);
        //        btn.BackgroundColor = Color.Green;
        //        context.IsTimerRunning = false;



        //        #region Assign Start and completion date property according to timer stopped
        //        InspectionStartDate = context.InspectionStartTime;
        //        InspectionCompletionDate = context.InspectionStopTime;
        //        #endregion


        //        #region Test region on fill the start date and end date
        //        // Fill the start date if inspection start date is null and fill the end date
        //        if (inspectionTime.InspectionStartDate == null)
        //        {
        //            if (Device.Idiom == TargetIdiom.Phone)
        //            {
        //                //this.PickerInspectionStartDatePhone.Text = context.InspectionStartTime.GetValueOrDefault().Date.ToShortDateString();
        //            }
        //            else
        //            {
        //                // this.PickerInspectionStartDateTablet.Text = context.InspectionStartTime.GetValueOrDefault().Date.ToShortDateString();
        //            }

        //            this.InspectionStartDate = context.InspectionStartTime.GetValueOrDefault().Date;
        //            if (Device.Idiom == TargetIdiom.Phone)
        //            {
        //                //this.PickerInspectionCompletionDatePhone.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
        //            }
        //            else
        //            {
        //                //this.PickerInspectionCompletionDateTablet.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
        //            }


        //            this.InspectionCompletionDate = context.InspectionStopTime.GetValueOrDefault().Date;
        //        }

        //        else
        //        {
        //            if (Device.Idiom == TargetIdiom.Phone)
        //            {
        //                //this.PickerInspectionStartDatePhone.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
        //            }
        //            else
        //            {
        //                //this.PickerInspectionStartDateTablet.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
        //            }
        //            this.InspectionStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).Date;
        //            if (Device.Idiom == TargetIdiom.Phone)
        //            {
        //                //this.PickerInspectionCompletionDatePhone.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
        //            }
        //            else
        //            {
        //                // this.PickerInspectionCompletionDateTablet.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
        //            }
        //            this.InspectionCompletionDate = context.InspectionStopTime.GetValueOrDefault().Date;
        //        }

        //        #endregion


        //        try
        //        {
        //            //Save in Local
        //            string key = "WorkOrderInspection:" + context.WorkorderID;
        //            WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(context));
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}

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
            #region ***** Next *****

            StackLayout TabViewSL = new StackLayout();
            // masterGrid.Children.Add(TabViewSL, 0, 4);
            #region **** MiscellaneousGrid ****

            Grid MiscellaneousGrid = new Grid { BackgroundColor = Color.White, MinimumWidthRequest = 550, Padding = 2 };
            MiscellaneousGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            StackLayout MiscelMasterSL = new StackLayout();
            MiscellaneousGrid.Children.Add(MiscelMasterSL, 0, 0);
            ScrollView MiscelSV = new ScrollView();
            MiscelMasterSL.Children.Add(MiscelSV);
            // StackLayout layout2;
            StackLayout layout2Test = new StackLayout();
            MiscelSV.Content = layout2Test;
            #endregion

            #region **** GroupSection ***

            Grid GroupSectionsGrid = new Grid { BackgroundColor = Color.White, HeightRequest = 550, Padding = 2 };
            GroupSectionsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            StackLayout GroupSecSl = new StackLayout();
            GroupSectionsGrid.Children.Add(GroupSecSl);

            ScrollView GroupSecSV = new ScrollView();
            GroupSecSl.Children.Add(GroupSecSV);
            StackLayout GroupSecSlCase1 = new StackLayout();
            StackLayout GroupSecSlCaseTest1 = new StackLayout();
            GroupSecSlCaseTest1.Children.Add(GroupSecSlCase1);
            GroupSecSV.Content = GroupSecSlCaseTest1;

            #endregion

            SfExpander GroupSecExpCase1;


            #endregion
            int Misce = 0;
            int GroupSec = 0;

            foreach (var item in distinctList)
            {
                StackLayout layout2 = new StackLayout();
                if (item.SectionID == 0 || item.SectionID == null)
                {
                    #region ****** Miscellaneous *******
                    Misce = Misce + 1;
                    View Question;
                    View Label;
                    ////View LabelHours;
                    View Layout;

                    //Grid sta;
                    switch (item.ResponseTypeName)
                    {
                        #region ***** Pass/Fail ****

                        case "Pass/Fail":
                            StackLayout PassFailSl = new StackLayout();
                            //StackLayout PassFailSlLavel = new StackLayout();
                            StackLayout PassFailSlButton = new StackLayout();

                            var PassFailgrid = new Grid() { HorizontalOptions = LayoutOptions.End, BindingContext = item };
                            PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 45 });
                            PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 45 });
                            PassFailSlButton.Children.Add(PassFailgrid);

                            // PassFailSl.Children.Add(PassFailSlLavel);
                            PassFailSl.Children.Add(PassFailSlButton);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, };
                            GenerateAnswerText(item);

                            var btnTruePF = new SfButton()
                            {
                                Text = "Pass",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                //FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 40,
                                WidthRequest = 40
                            };
                            var btnFalsePF = new SfButton()
                            {
                                Text = "Fail",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                //FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 40,
                                WidthRequest=40
                            };
                            btnTruePF.Clicked += BtnTrue_Clicked;
                            btnFalsePF.Clicked += BtnFalse_Clicked;
                            PassFailgrid.Children.Add(Question, 0, 0);
                            PassFailgrid.Children.Add(Label, 0, 0);
                            PassFailgrid.Children.Add(btnTruePF, 1, 0);
                            PassFailgrid.Children.Add(btnFalsePF, 2, 0);

                            layout2.Children.Add(PassFailSl);
                            switch (item.AnswerDescription)
                            {
                                case "":
                                    break;
                                case "Pass":
                                    BtnTrue_Clicked(btnTruePF, null);
                                    break;
                                case "Fail":
                                    this.CreateWorkorderButtonSL.IsVisible = true;

                                    BtnFalse_Clicked(btnFalsePF, null);
                                    break;
                            }
                            break;
                        #endregion

                        #region ******* Yes/No/N/A ******

                        case "Yes/No/N/A":
                            StackLayout YesNoSl = new StackLayout();
                            //StackLayout YesNoSlLavel = new StackLayout();
                            StackLayout YesNoSlButton = new StackLayout();

                            Grid YesNogrid = new Grid() { BindingContext = item };
                            YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 38 });
                            YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 38 });
                            YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 38 });
                            YesNoSlButton.Children.Add(YesNogrid);

                            //YesNoSl.Children.Add(YesNoSlLavel);
                            YesNoSl.Children.Add(YesNoSlButton);
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };

                            var btnTrue = new SfButton()
                            {
                                Text = "Yes",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 36
                            };
                            var btnFalse = new SfButton()
                            {
                                Text = "No",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 36
                            };
                            var btnNA = new SfButton()
                            {
                                Text = "NA",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 36
                            };

                            //Bind the ResponseSubType in Buttons
                            btnTrue.BindingContext = item.ResponseSubType;
                            btnFalse.BindingContext = item.ResponseSubType;
                            btnNA.BindingContext = item.ResponseSubType;
                            // TODO: Add the NA button over here
                            btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                            btnFalse.Clicked += BtnNo_Clicked;
                            btnNA.Clicked += BtnNA_Clicked;
                            YesNogrid.Children.Add(Question, 0, 0);
                            YesNogrid.Children.Add(Label, 0, 0);
                            Grid.SetColumnSpan(Label, 3);
                            YesNogrid.Children.Add(btnTrue, 2, 0);
                            YesNogrid.Children.Add(btnFalse, 3, 0);
                            YesNogrid.Children.Add(btnNA, 4, 0);

                            layout2.Children.Add(YesNoSl);
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
                                    //this.btnCreateWorkorder.IsVisible = true;
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
                                        CreateWorkorderButtonSL.IsVisible = true;
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
                                        CreateWorkorderButtonSL.IsVisible = true;
                                        var MGrid = this.CreateWorkorderButtonSL.Parent as Grid;
                                        MGrid.Children[3].IsVisible = true;
                                        break;

                                }
                            }
                            #endregion

                            break;
                        #endregion

                        #region ***** Standard Range ***
                        case "Standard Range":
                            StackLayout SRangeSl = new StackLayout();
                            StackLayout SRangeSlLavel = new StackLayout();
                            var SRangegrid = new Grid() { BindingContext = item };
                            SRangegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            SRangegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                            SRangeSlLavel.Children.Add(SRangegrid);
                            // StackLayout SRangeSlButton = new StackLayout();

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            Layout = new Entry() { Keyboard = Keyboard.Numeric };
                            SRangeSl.Children.Add(SRangeSlLavel);
                            GenerateAnswerText(item);
                            SfBorder RangeBor = new SfBorder() { CornerRadius = 5 };

                            (Layout as Entry).BindingContext = new Range() { MaxRange = item.MaxRange, MinRange = item.MinRange };
                            RangeBor.Content = Layout;
                            SRangegrid.Children.Add(Question, 0, 0);
                            SRangegrid.Children.Add(Label, 0, 0);
                            SRangegrid.Children.Add(RangeBor, 1, 0);
                            (Layout as Entry).TextChanged += StandardRange_TextChanged;
                            (Layout as Entry).Text = item.AnswerDescription;
                            layout2.Children.Add(SRangeSl);
                            if (!string.IsNullOrWhiteSpace(item.AnswerDescription))
                            {
                                if (Convert.ToDecimal(item.AnswerDescription) >= item.MinRange && Convert.ToDecimal(item.AnswerDescription) <= item.MaxRange)
                                {
                                    this.CreateWorkorderButtonSL.IsVisible = false;
                                }
                                else
                                {
                                    this.CreateWorkorderButtonSL.IsVisible = true;
                                }
                            }
                            break;
                        #endregion

                        #region ****** Count *****
                        case "Count":
                            StackLayout CountSl = new StackLayout();
                            StackLayout CountSlLavel = new StackLayout();
                            Grid CountGrid = new Grid() { BindingContext = item };

                            CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 100 });
                            CountSlLavel.Children.Add(CountGrid);
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };
                            GenerateAnswerText(item);

                            Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };
                            (Layout as MyEntry).Text = item.AnswerDescription;
                            (Layout as MyEntry).TextChanged += OnlyNumeric_TextChanged;

                            CountGrid.Children.Add(Question, 0, 0);
                            CountGrid.Children.Add(Label, 0, 0);
                            CountGrid.Children.Add(Layout, 1, 0);
                            CountSl.Children.Add(CountSlLavel);
                            layout2.Children.Add(CountSl);

                            break;
                        #endregion

                        #region **** Text ****
                        case "Text":
                            StackLayout TextSl = new StackLayout();
                            StackLayout TextSlLavel = new StackLayout();
                            var Textgrid = new Grid() { BindingContext = item };
                            Textgrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            Textgrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            TextSlLavel.Children.Add(Textgrid);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };


                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };

                            GenerateAnswerText(item);

                            Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                            (Layout as CustomEditor).Text = item.AnswerDescription;
                            SfBorder TextSfBorder = new SfBorder
                            {
                                BorderColor = Color.Black,
                                BorderWidth = 1,
                                CornerRadius = 5
                            };
                            TextSfBorder.Content = Layout;
                            Textgrid.Children.Add(Question, 0, 0);
                            Textgrid.Children.Add(Label, 0, 0);
                            Textgrid.Children.Add(TextSfBorder, 0, 1);

                            TextSl.Children.Add(TextSlLavel);

                            layout2.Children.Add(TextSl);
                            break;
                        #endregion

                        #region ****** Multiple Choice******
                        case "Multiple Choice":
                            StackLayout MChoiceSl = new StackLayout();
                            StackLayout MChoiceSlLavel = new StackLayout();

                            MChoiceSl.Children.Add(MChoiceSlLavel);
                            Grid MChoiceGrid = new Grid() { BindingContext = item };
                            MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            MChoiceSlLavel.Children.Add(MChoiceGrid);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);

                            MChoiceSlLavel.Children.Add(Label);
                            Layout = new CustomPicker() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Image = (Device.RuntimePlatform == Device.UWP) ? "Assets/unnamed" : "unnamed" };

                            if (!string.IsNullOrWhiteSpace(item.Option1))
                                (Layout as CustomPicker).Items.Add(item.Option1);
                            if (!string.IsNullOrWhiteSpace(item.Option2))
                                (Layout as CustomPicker).Items.Add(item.Option2);
                            if (!string.IsNullOrWhiteSpace(item.Option3))
                                (Layout as CustomPicker).Items.Add(item.Option3);
                            if (!string.IsNullOrWhiteSpace(item.Option4))
                                (Layout as CustomPicker).Items.Add(item.Option4);
                            if (!string.IsNullOrWhiteSpace(item.Option5))
                                (Layout as CustomPicker).Items.Add(item.Option5);
                            if (!string.IsNullOrWhiteSpace(item.Option6))
                                (Layout as CustomPicker).Items.Add(item.Option6);
                            if (!string.IsNullOrWhiteSpace(item.Option7))
                                (Layout as CustomPicker).Items.Add(item.Option7);
                            if (!string.IsNullOrWhiteSpace(item.Option8))
                                (Layout as CustomPicker).Items.Add(item.Option8);
                            if (!string.IsNullOrWhiteSpace(item.Option9))
                                (Layout as CustomPicker).Items.Add(item.Option9);
                            if (!string.IsNullOrWhiteSpace(item.Option10))
                                (Layout as CustomPicker).Items.Add(item.Option10);

                            var index = (Layout as CustomPicker).Items.IndexOf(item.AnswerDescription);
                            (Layout as CustomPicker).SelectedIndex = index;

                            MChoiceGrid.Children.Add(Question, 0, 0);
                            MChoiceGrid.Children.Add(Label, 0, 0);
                            MChoiceGrid.Children.Add(Layout, 1, 0);

                            MChoiceSlLavel.Children.Add(MChoiceGrid);
                            MChoiceSl.Children.Add(MChoiceSlLavel);
                            layout2.Children.Add(MChoiceSl);


                            break;
                        #endregion

                        #region ***** None *****
                        case "None":
                            StackLayout NoneSl = new StackLayout();
                            StackLayout NoneSlLavel = new StackLayout();
                            NoneSl.Children.Add(NoneSlLavel);
                            var Nonegrid = new Grid() { BindingContext = item };
                            Nonegrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            NoneSlLavel.Children.Add(Nonegrid);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);
                            Nonegrid.Children.Add(Question, 0, 0);
                            Nonegrid.Children.Add(Label, 0, 0);
                            layout2.Children.Add(NoneSl);

                            break;
                            #endregion
                    }
                    #region ***** Buttons ***

                    var btnsave = new Button() { BackgroundColor = Color.White, ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/saveicon1.png" : "saveicon1.png" };
                    btnsave.Clicked += Btnsave_Clicked;

                    var btnDelete = new Button() { BackgroundColor = Color.White, ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/delIcon1.png" : "delIcon1.png", StyleId = item.InspectionID.ToString() };
                    btnDelete.Clicked += BtnDelete_Clicked;
                    if (InspectionRRight == "N")
                    {
                        btnDelete.IsVisible = false;
                    }
                    else if (InspectionRRight == "V")
                    {
                        btnDelete.IsEnabled = false;
                    }
                    StackLayout Case1SL = new StackLayout();
                    layout2.Children.Add(Case1SL);
                    Grid Case1Grid = new Grid();
                    Case1Grid.Padding = new Thickness(0, 0, 0, -12);
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                    Case1Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Case1Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Case1SL.Children.Add(Case1Grid);


                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 0 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        Case1Grid.Children.Add(imageView, 0, 0);
                        Grid.SetColumnSpan(imageView, 4);
                        if (byteImg.Length > 0)
                        {
                            imageView.HeightRequest = 150;
                        }
                        var addSignatureButton = new Button() { BackgroundColor = Color.White,  ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/signature.png" : "signature.png" };
                        addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        Case1Grid.Children.Add(btnsave, 2, 1);
                        Case1Grid.Children.Add(btnDelete, 3, 1);
                        Case1Grid.Children.Add(addSignatureButton, 4, 1);
                        var s = item.SignaturePath;
                    }
                    else
                    {
                        Case1Grid.Children.Add(btnsave, 3, 1);
                        Case1Grid.Children.Add(btnDelete, 4, 1);
                    }
                    #endregion

                    var estimatedHourTitleLabel = WebControlTitle.GetTargetNameByTitleName("EstimatedHours");
                    var estimatedHourLabel = item.EstimatedHours;

                    Label Case1lbl = new Label
                    {
                        Text = estimatedHourTitleLabel + ": " + estimatedHourLabel,
                        TextColor = Color.FromHex("#006de0"),
                        VerticalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    Case1Grid.Children.Add(Case1lbl, 0, 1);
                    Grid.SetColumnSpan(Case1lbl, 3);
                    BoxView lineBox = new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = Color.Black,
                    };
                    layout2.Children.Add(lineBox);
                    #endregion
                    #endregion
                    layout2Test.Children.Add(layout2);
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
                    GroupSec = GroupSec + 1;
                    GroupSecExpCase1 = new SfExpander();
                    GroupSecExpCase1.Header = new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("#006de0"),
                        BackgroundColor = Color.WhiteSmoke,
                        HeightRequest = 40,
                        Text = commonSections[0].SectionName,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    StackLayout ItemListCase1Sl = new StackLayout();
                    GroupSecExpCase1.Content = ItemListCase1Sl;
                    //StackLayout ItemsCase1Sl = new StackLayout();
                    //ItemListCase1Sl.Children.Add(ItemsCase1Sl);
                    foreach (var item1 in commonSections)
                    {
                        //View Question="";
                        //View Label1 = "";
                        //View Layout = "";

                        //StackLayout sta;
                        //Grid sta;
                        switch (item1.ResponseTypeName)
                        {
                            case "Pass/Fail":
                                StackLayout PassFailSl = new StackLayout();
                                StackLayout PassFailSlButton = new StackLayout();
                                PassFailSl.Children.Add(PassFailSlButton);

                                Grid PassFailgrid = new Grid() { BindingContext = item };
                                PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                PassFailSlButton.Children.Add(PassFailgrid);
                                var Questions1 = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                var Label1 = new Label { Text = item1.InspectionDescription, HorizontalTextAlignment = TextAlignment.Start, Font = Font.SystemFontOfSize(14, FontAttributes.None), BindingContext = item1, TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, };

                                GenerateAnswerText(item1);
                                var btnTruePF = new SfButton()
                                {
                                    Text = "Pass",
                                    TextColor = Color.Black,
                                    FontSize = 10,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 40,
                                    WidthRequest = 40
                                };
                                var btnFalsePF = new SfButton()
                                {
                                    Text = "Fail",
                                    TextColor = Color.Black,
                                    FontSize = 10,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 40,
                                    WidthRequest = 40
                                };


                                btnTruePF.Clicked += BtnTrue_Clicked;
                                btnFalsePF.Clicked += BtnFalse_Clicked;
                                PassFailgrid.Children.Add(Questions1, 0, 0);
                                PassFailgrid.Children.Add(Label1, 0, 0);
                                Grid.SetColumnSpan(Label1, 2);
                                PassFailgrid.Children.Add(btnTruePF, 2, 0);
                                PassFailgrid.Children.Add(btnFalsePF, 3, 0);
                                ItemListCase1Sl.Children.Add(PassFailSl);
                                switch (item1.AnswerDescription)
                                {
                                    case "":
                                        break;
                                    case "Pass":
                                        BtnTrue_Clicked(btnTruePF, null);
                                        break;
                                    case "Fail":
                                        this.CreateWorkorderButtonSL.IsVisible = true;
                                        BtnFalse_Clicked(btnFalsePF, null);
                                        break;

                                }
                                break;

                            case "Standard Range":
                                StackLayout SRangeSl = new StackLayout();
                                StackLayout SRangeSlButton = new StackLayout();
                                SRangeSl.Children.Add(SRangeSlButton);
                                Grid sta = new Grid() { BindingContext = item };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                                SRangeSlButton.Children.Add(sta);

                                var Questions = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                var Label1s = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                SfBorder RangeBor = new SfBorder() { CornerRadius = 5 };

                                Entry Layouts = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };

                                Layouts.BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                                RangeBor.Content = Layouts;
                                sta.Children.Add(Questions, 0, 0);
                                sta.Children.Add(Label1s, 0, 0);
                                sta.Children.Add(RangeBor, 1, 0);
                                Layouts.TextChanged += StandardRange_TextChanged;
                                Layouts.Text = item1.AnswerDescription;
                                ItemListCase1Sl.Children.Add(SRangeSl);
                                if (!string.IsNullOrWhiteSpace(item1.AnswerDescription))
                                {
                                    if (Convert.ToDecimal(item1.AnswerDescription) >= item1.MinRange && Convert.ToDecimal(item1.AnswerDescription) <= item1.MaxRange)
                                    {
                                        this.CreateWorkorderButtonSL.IsVisible = false;
                                    }
                                    else
                                    {
                                        this.CreateWorkorderButtonSL.IsVisible = true;
                                    }
                                }

                                break;

                            case "Yes/No/N/A":
                                StackLayout YesNoSl = new StackLayout();
                                // StackLayout YesNoSlLavel = new StackLayout();
                                StackLayout YesNoSlButton = new StackLayout();
                                Grid YesNogrid = new Grid() { BindingContext = item };
                                YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                YesNoSlButton.Children.Add(YesNogrid);

                                // YesNoSl.Children.Add(YesNoSlLavel);
                                YesNoSl.Children.Add(YesNoSlButton);
                                var Questions1y = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                var Label1y = new Label { Text = item1.InspectionDescription, HorizontalTextAlignment = TextAlignment.Start, Font = Font.SystemFontOfSize(14, FontAttributes.None), BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, TextColor = Color.Black };
                                // YesNoSlLavel.Children.Add(Label1);

                                GenerateAnswerText(item1);

                                var btnTrue = new SfButton()
                                {
                                    Text = "Yes",
                                    TextColor = Color.Black,
                                    FontSize = 11,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 36
                                };
                                var btnFalse = new SfButton()
                                {
                                    Text = "No",
                                    TextColor = Color.Black,
                                    FontSize = 11,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 36
                                };
                                var btnNA = new SfButton()
                                {
                                    Text = "NA",
                                    TextColor = Color.Black,
                                    FontSize = 11,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 36
                                };

                                //Bind the ResponseSubType in Buttons
                                btnTrue.BindingContext = item1.ResponseSubType;
                                btnFalse.BindingContext = item1.ResponseSubType;
                                btnNA.BindingContext = item1.ResponseSubType;

                                //TODO: Add the NA button over here
                                btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                btnFalse.Clicked += BtnNo_Clicked;
                                btnNA.Clicked += BtnNA_Clicked;
                                YesNogrid.Children.Add(Questions1y, 0, 0);
                                YesNogrid.Children.Add(Label1y, 0, 0);
                                //Grid.SetColumnSpan(Label1y, 2);
                                YesNogrid.Children.Add(btnTrue, 1, 0);
                                YesNogrid.Children.Add(btnFalse, 2, 0);
                                YesNogrid.Children.Add(btnNA, 3, 0);
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
                                            CreateWorkorderButtonSL.IsVisible = true;
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
                                            CreateWorkorderButtonSL.IsVisible = true;
                                            var MGrid = this.CreateWorkorderButtonSL.Parent as Grid;
                                            MGrid.Children[3].IsVisible = true;
                                            break;

                                    }
                                }
                                #endregion


                                ItemListCase1Sl.Children.Add(YesNoSl);

                                break;

                            case "Count":
                                StackLayout CountSl = new StackLayout();
                                StackLayout CountSlLavel = new StackLayout();
                                CountSl.Children.Add(CountSlLavel);
                                Grid CountGrid = new Grid() { BindingContext = item };

                                CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                                CountSlLavel.Children.Add(CountGrid);

                                var Questionc = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                var Label1c = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };


                                GenerateAnswerText(item1);

                                var Layoutc = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };
                                (Layoutc as MyEntry).Text = item1.AnswerDescription;
                                (Layoutc as MyEntry).TextChanged += OnlyNumeric_TextChanged;

                                CountGrid.Children.Add(Questionc, 0, 0);
                                CountGrid.Children.Add(Label1c, 0, 0);
                                CountGrid.Children.Add(Layoutc, 1, 0);

                                ItemListCase1Sl.Children.Add(CountSl);

                                break;
                            case "Text":
                                StackLayout TextSl = new StackLayout();
                                StackLayout TextSlLavel = new StackLayout();
                                TextSl.Children.Add(TextSlLavel);

                                var Textgrid = new Grid() { BindingContext = item };
                                Textgrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                Textgrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                TextSlLavel.Children.Add(Textgrid);

                                var Questiont = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                var Label1t = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };
                                GenerateAnswerText(item1);

                                var Layoutt = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                                (Layoutt as CustomEditor).Text = item1.AnswerDescription;

                                SfBorder TextSfBorder = new SfBorder
                                {
                                    BorderColor = Color.Black,
                                    BorderWidth = 1,
                                    CornerRadius = 5
                                };
                                TextSfBorder.Content = Layoutt;
                                Textgrid.Children.Add(Questiont, 0, 0);
                                Textgrid.Children.Add(Label1t, 0, 0);
                                Textgrid.Children.Add(TextSfBorder, 0, 1);

                                ItemListCase1Sl.Children.Add(TextSl);

                                break;

                            case "Multiple Choice":

                                StackLayout MChoiceSl = new StackLayout();
                                StackLayout MChoiceSlLavel = new StackLayout();
                                MChoiceSl.Children.Add(MChoiceSlLavel);
                                Grid MChoiceGrid = new Grid() { BindingContext = item };
                                MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                                MChoiceSlLavel.Children.Add(MChoiceGrid);

                                var Questionm = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                var Label1m = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                var Layoutm = new CustomPicker() { WidthRequest = 100, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Image = (Device.RuntimePlatform == Device.UWP) ? "Assets/unnamed.png" : "unnamed.png"};

                                if (!string.IsNullOrWhiteSpace(item1.Option1))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option1);
                                if (!string.IsNullOrWhiteSpace(item1.Option2))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option2);
                                if (!string.IsNullOrWhiteSpace(item1.Option3))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option3);
                                if (!string.IsNullOrWhiteSpace(item1.Option4))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option4);
                                if (!string.IsNullOrWhiteSpace(item1.Option5))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option5);
                                if (!string.IsNullOrWhiteSpace(item1.Option6))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option6);
                                if (!string.IsNullOrWhiteSpace(item1.Option7))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option7);
                                if (!string.IsNullOrWhiteSpace(item1.Option8))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option8);
                                if (!string.IsNullOrWhiteSpace(item1.Option9))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option9);
                                if (!string.IsNullOrWhiteSpace(item1.Option10))
                                    (Layoutm as CustomPicker).Items.Add(item1.Option10);


                                var index = (Layoutm as CustomPicker).Items.IndexOf(item1.AnswerDescription);
                                (Layoutm as CustomPicker).SelectedIndex = index;


                                MChoiceGrid.Children.Add(Questionm, 0, 0);
                                MChoiceGrid.Children.Add(Label1m, 0, 0);
                                MChoiceGrid.Children.Add(Layoutm, 1, 0);
                                ItemListCase1Sl.Children.Add(MChoiceSl);

                                break;

                            case "None":
                                StackLayout NoneSl = new StackLayout();
                                StackLayout NoneSlLavel = new StackLayout();
                                NoneSl.Children.Add(NoneSlLavel);
                                var Nonegrid = new Grid() { BindingContext = item };
                                Nonegrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                NoneSlLavel.Children.Add(Nonegrid);

                                var Questionn = new Label { Text = "", Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                var Label1n = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, BindingContext = item1, };

                                GenerateAnswerText(item1);
                                Nonegrid.Children.Add(Questionn, 0, 0);
                                Nonegrid.Children.Add(Label1n, 0, 0);
                                ItemListCase1Sl.Children.Add(NoneSl);

                                break;
                        }
                        GroupSecSlCase1.Children.Add(GroupSecExpCase1);
                    }

                    var btnsave = new Button() { BackgroundColor = Color.White, HeightRequest = 40, VerticalOptions = LayoutOptions.FillAndExpand, ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/saveicon1.png" : "saveicon1.png" };
                    btnsave.Clicked += BtnSaveSection_Clicked;
                    string Sid = item.SectionID.ToString();
                    var btnDelete = new Button() { BackgroundColor = Color.White, VerticalOptions = LayoutOptions.FillAndExpand, ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/delIcon1.png" : "delIcon1.png", StyleId = Sid };
                    btnDelete.Clicked += BtnSectionDelete_Clicked;
                    if (InspectionRRight == "N")
                    {
                        btnDelete.IsVisible = false;
                    }
                    else if (InspectionRRight == "V")
                    {
                        btnDelete.IsEnabled = false;
                    }
                    StackLayout Case1SL = new StackLayout();

                    Grid Case1Grid = new Grid();
                    Case1Grid.Padding = new Thickness(0, 1, 0, -10);
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 45 });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 45 });
                    Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 45 });
                    Case1Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Case1Grid.RowDefinitions.Add(new RowDefinition { Height = 45 });
                    Case1SL.Children.Add(Case1Grid);
                    layout2.Children.Add(Case1SL);
                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 0 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        Case1Grid.Children.Add(imageView, 0, 0);
                        Grid.SetColumnSpan(imageView, 4);
                        if (byteImg.Length > 0)
                        {
                            imageView.HeightRequest = 150;
                        }

                        //var addSignatureButton = new SfButton() {  BackgroundColor = Color.LightGray, ShowIcon = true, ImageSource = "signature.png"};
                        var addSignatureButton = new Button() { BackgroundColor = Color.White, ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/signature.png" : "signature.png" };
                        addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        // layout2.Children.Add(addSignatureButton);
                        Case1Grid.Children.Add(btnsave, 2, 1);
                        Case1Grid.Children.Add(btnDelete, 3, 1);
                        Case1Grid.Children.Add(addSignatureButton, 4, 1);

                        var s = item.SignaturePath;
                    }
                    else
                    {
                        Case1Grid.Children.Add(btnsave, 3, 1);
                        Case1Grid.Children.Add(btnDelete, 4, 1);
                    }
                    #endregion

                    var estimatedHourTitleLabel = WebControlTitle.GetTargetNameByTitleName("EstimatedHours");
                    var estimatedHourLabel = item.EstimatedHours;

                    Label Case1lbl = new Label
                    {
                        Text = estimatedHourTitleLabel + ": " + estimatedHourLabel,
                        TextColor = Color.FromHex("#006de0"),
                        Margin = new Thickness(0, 0, 0, 0),
                        VerticalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };
                    Case1Grid.Children.Add(Case1lbl, 0, 1);
                    Grid.SetColumnSpan(Case1lbl, 3);
                    ItemListCase1Sl.Children.Add(Case1Grid);

                }
            }

            #region *****Expanded Collapsed****

            VisualStateGroupList visualStateGroupList = new VisualStateGroupList();
            VisualStateGroup commonStateGroup = new VisualStateGroup();

            VisualState expanded = new VisualState
            {
                Name = "Expanded"
            };
            VisualState collapsed = new VisualState
            {
                Name = "Collapsed"
            };
            commonStateGroup.States.Add(expanded);
            commonStateGroup.States.Add(collapsed);

            visualStateGroupList.Add(commonStateGroup);
            // VisualStateManager.SetVisualStateGroups(GroupSecExpCase1, visualStateGroupList);

            #endregion


            SfTabView tabView = new SfTabView
            {
                TabWidthMode = TabWidthMode.BasedOnText,
                BackgroundColor = Color.WhiteSmoke,
                DisplayMode = TabDisplayMode.Text,
                Margin = new Thickness(0, -5, 0, 0)
            };

            TabViewSL.Children.Add(tabView);
            var tabItems = new TabItemCollection
            {
                new SfTabItem()
                {
                    Title = "Group Sections ("+GroupSec+")",
                    Content = GroupSectionsGrid

                },
                new SfTabItem()
                {
                     Title = "Miscellaneous Question ("+Misce+")",
                    Content = MiscellaneousGrid
                }
            };

            var MiscellGrid = MiscellaneousGrid.Height;
            layout2Test.HeightRequest = MinimumHeightRequest = 1000;

            var GroupSGrid = MiscellaneousGrid.Height;
            GroupSecSlCaseTest1.HeightRequest = 2000;

            tabView.Items = tabItems;
            MainLayout.Children.Add(tabView);
        }
        private async void AddSignatureButton_Clicked(object sender, EventArgs e)
        {
            var data = (sender as Button).Parent;
            var parentView = ((sender as Button).Parent as Grid);

            var image = parentView.Children[0] as CustomImage;
            image.HeightRequest = 150;
            var signaturePad = new SignaturePage(image);
            signaturePad.OnSignatureDrawn += SignaturePad_OnSignatureDrawn;
            await Navigation.PushPopupAsync(signaturePad);
        }

        //private async void AddSignatureButton_Clicked(object sender, EventArgs e)
        //{
        //    var data = (sender as SfButton).Parent;
        //    var parentView = ((sender as SfButton).Parent as Grid);
        //    // var image1 = parentView.Children[parentView.Children.Count - 4] as CustomImage;

        //    var image = parentView.Children[1] as CustomImage;
        //    image.HeightRequest = 150;
        //    var signaturePad = new SignaturePage(image);
        //    signaturePad.OnSignatureDrawn += SignaturePad_OnSignatureDrawn;
        //    await Navigation.PushPopupAsync(signaturePad);
        //}

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
            switch (item.ResponseTypeName)
            {
                case "Yes/No/N/A":
                    if (item.AnswerDescription == "No")
                    {
                        this.AnswerText.Append(item.InspectionDescription);
                        this.AnswerText.Append(": ");
                        this.AnswerText.Append(item.AnswerDescription);
                        this.AnswerText.Append(System.Environment.NewLine);
                    }

                    break;
                case "Pass/Fail":
                    if (item.AnswerDescription == "Fail")
                    {
                        this.AnswerText.Append(item.InspectionDescription);
                        this.AnswerText.Append(": ");
                        this.AnswerText.Append(item.AnswerDescription);
                        this.AnswerText.Append(System.Environment.NewLine);
                    }
                    break;
                case "Standard Range":
                    if (string.IsNullOrWhiteSpace(item.AnswerDescription))
                    {
                        if (item.MinRange != null && item.MaxRange != null)
                        {
                            if (!string.IsNullOrWhiteSpace(item.AnswerDescription))
                            {
                                if (decimal.Parse(item.AnswerDescription) <= item.MinRange && decimal.Parse(item.AnswerDescription) >= item.MaxRange)
                                {
                                    this.AnswerText.Append(item.InspectionDescription);
                                    this.AnswerText.Append(": ");
                                    this.AnswerText.Append(item.AnswerDescription);
                                    this.AnswerText.Append(System.Environment.NewLine);
                                }
                            }
                        }

                    }
                    break;

                case "Multiple Choice":
                    if (string.IsNullOrWhiteSpace(item.AnswerDescription))
                    {
                        this.AnswerText.Append(item.InspectionDescription);
                        this.AnswerText.Append(": ");
                        this.AnswerText.Append(item.AnswerDescription);
                        this.AnswerText.Append(System.Environment.NewLine);
                    }
                    break;

                case "Text":
                    if (string.IsNullOrWhiteSpace(item.AnswerDescription))
                    {
                        this.AnswerText.Append(item.InspectionDescription);
                        this.AnswerText.Append(": ");
                        this.AnswerText.Append(item.AnswerDescription);
                        this.AnswerText.Append(System.Environment.NewLine);
                    }
                    break;
                case "Count":
                    if (string.IsNullOrWhiteSpace(item.AnswerDescription))
                    {
                        this.AnswerText.Append(item.InspectionDescription);
                        this.AnswerText.Append(": ");
                        this.AnswerText.Append(item.AnswerDescription);
                        this.AnswerText.Append(System.Environment.NewLine);
                    }
                    break;
            }

        }

        private void BtnTrue_Clicked(object sender, EventArgs e)
        {
            var btn = sender as SfButton;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#00cc66");
            var grid = (sender as SfButton).Parent as Grid;
            var btnFalse = grid.Children[3] as SfButton;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnFalse.BackgroundColor = Color.LightGray;

        }

        private void BtnFalse_Clicked(object sender, EventArgs e)
        {
            var btn = sender as SfButton;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#ff5050");

            var grid = (sender as SfButton).Parent as Grid;
            var btnFalse = grid.Children[2] as SfButton;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnFalse.BackgroundColor = Color.LightGray;

        }

        private void BtnYes_Clicked(object sender, EventArgs e)
        {
            var btn = sender as SfButton;
            var btn1 = btn.Parent;
            var grid = (sender as SfButton).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnFalse = grid.Children[3] as SfButton;
            var btnNA = grid.Children[4] as SfButton;
            //TODO: Get the button color from the btn object set the inverse color over here.
            if (responseSubType)
            {
                btn.BackgroundColor = Color.FromHex("#00cc66");

            }
            else
            {
                btn.BackgroundColor = Color.FromHex("#ff5050");

            }

            btnFalse.BackgroundColor = Color.LightGray;
            btnNA.BackgroundColor = Color.LightGray;

        }

        private void BtnNo_Clicked(object sender, EventArgs e)
        {
            var btn = sender as SfButton;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#ff5050");

            var grid = (sender as SfButton).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnYes = grid.Children[2] as SfButton;
            var btnNA = grid.Children[4] as SfButton;
            //TODO: Get the button color from the btn object set the inverse color over here.
            if (!responseSubType)
            {
                btn.BackgroundColor = Color.FromHex("#00cc66");

            }
            else
            {
                btn.BackgroundColor = Color.FromHex("#ff5050");

            }

            btnYes.BackgroundColor = Color.LightGray;
            btnNA.BackgroundColor = Color.LightGray;

        }

        private void BtnNA_Clicked(object sender, EventArgs e)
        {
            var btn = sender as SfButton;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#ffcc66");

            var grid = (sender as SfButton).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnTrue = grid.Children[2] as SfButton;
            var btnFalse = grid.Children[3] as SfButton;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnTrue.BackgroundColor = Color.LightGray;
            btnFalse.BackgroundColor = Color.LightGray;

        }

        private void PassFail_Clicked(object sender, EventArgs e)
        {
            var btn = sender as SfButton;

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
            var btn = sender as SfButton;

            if (btn.Text == "Yes")
            {
                btn.Text = "No";
                btn.BackgroundColor = Color.FromHex("#ff5050");
            }

            else
            {
                btn.Text = "Yes";
                btn.BackgroundColor = Color.FromHex("#00cc66");
            }
        }

        private void StandardRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text
            var data = (sender as Entry).Parent as Grid;
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
                        e1.BackgroundColor = Color.FromHex("#ff5050");
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
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);
                CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
                if (CC.workOrderEmployee.Count == 0 && CC.workorderContractor.Count == 0)
                {

                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("PleaseFirstAddEmployeeORContractor"), TimeSpan.FromSeconds(2));
                    UserDialogs.Instance.HideLoading();
                    return;

                }
                List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();
                List<InspectionTOAnswers> liststartAnswer = new List<InspectionTOAnswers>();
                List<InspectionTOAnswers> listcompletionAnswer = new List<InspectionTOAnswers>();



                var test = (sender as Button).Parent.Parent;
                var stacklayout = (sender as Button).Parent.Parent as StackLayout;

                //var context = (sender as Button).CommandParameter as List<ExistingInspections>;

                List<InspectionAnswer> listAnswer = new List<InspectionAnswer>();
                int viewCount = 0;


                for (int i = 0; i < stacklayout.Children.Count - 1; i++) // run it only for grid
                {
                    try
                    {
                        var stacklayout1 = stacklayout.Children[i] as StackLayout;
                        if (stacklayout1 == null)
                        {

                        }
                        else
                        {
                            var LalGrid = stacklayout1.Children[0] as StackLayout;
                            var LalGrid1 = LalGrid.Children[0] as Grid;
                            var context = (LalGrid1.Children[1] as Label).BindingContext as ExistingInspections;

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

                    signatureImageView = ((sender as Button).Parent as Grid).Children[0] as CustomImage;
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


                var serviceStatus = await ViewModel._inspectionService.AnswerInspection(answer);

                #endregion



                #region Save Inspection Time to server

                // 1st Employee/Contractor Layout//

                Label employeecontrcatorids = new Label();

                try
                {
                    var grid = ParentLayout.Children[0] as Grid;
                    var stacklayoutg1 = grid.Children[2] as StackLayout;
                    var SVLayout = stacklayoutg1.Children[0] as ScrollView;
                    var stacklayout1 = SVLayout.Children[0] as StackLayout;

                    for (int i = 0; i < stacklayout1.Children.Count; i++)
                    {
                        string EmpLaborCraftID = "0";
                        string ContLaborCraftID = "0";
                        int? WorkOrderInspectionTimeIDs = null;
                        var FrameLayout = stacklayout1.Children[i] as Frame;
                        if (!string.IsNullOrWhiteSpace(FrameLayout.StyleId))
                        {
                            WorkOrderInspectionTimeIDs = Convert.ToInt32(FrameLayout.StyleId);
                        }
                        var Fstacklayout = FrameLayout.Children[0] as StackLayout;
                        var Fstacklayout1 = Fstacklayout.Children[0] as StackLayout;

                        var Fstacklayout2 = Fstacklayout1.Children[0] as Grid;

                        var Hrsstacklayout3 = Fstacklayout2.Children[3] as StackLayout;
                        var HrsGrid = Hrsstacklayout3.Children[0] as Grid;
                        var Hrsgridlayout = HrsGrid.Children[0] as SfBorder;
                        var HoursEntryValue = Hrsgridlayout.Children[0] as Entry;
                        var HoursTextType = HoursEntryValue.IsReadOnly;
                        var HoursEntryValue1 = HoursEntryValue.Text;

                        var Mistacklayout3 = Fstacklayout2.Children[4] as StackLayout;
                        var MiGrid = Mistacklayout3.Children[0] as Grid;
                        var Migridlayout = MiGrid.Children[0] as SfBorder;
                        var MinuteEntryValue = Migridlayout.Children[0] as Entry;
                        var MinTextType = HoursEntryValue.IsReadOnly;
                        var MinuteEntryValue1 = MinuteEntryValue.Text;

                        var Datelayout1 = Fstacklayout.Children[1] as StackLayout;
                        var DateGrid1 = Datelayout1.Children[0] as Grid;
                        var DateSLt1 = DateGrid1.Children[0] as StackLayout;
                        var DateBodder1 = DateSLt1.Children[1] as SfBorder;
                        var StartdateValue = DateBodder1.Children[0] as CustomDatePicker1;
                        // var StartdateValue1 = DateTime.Parse(StartdateValue.SelectedDate.ToString());

                        var DateSLt2 = DateGrid1.Children[1] as StackLayout;
                        var DateBodder2 = DateSLt2.Children[1] as SfBorder;
                        var CompletionDateValue = DateBodder2.Children[0] as CustomDatePicker1;
                        // var CompletionDateValue1 = DateTime.Parse(CompletionDateValue.SelectedDate.ToString());
                        if (StartdateValue != null)
                        {
                            this.InspectionStartDate = StartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(StartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        }
                        else
                        {
                            this.InspectionStartDate = null;
                        }
                        if (CompletionDateValue != null)
                        {
                            this.InspectionCompletionDate = CompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(CompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        }
                        else
                        {
                            this.InspectionCompletionDate = null;
                        }


                        var Lablestacklayout = Fstacklayout2.Children[0] as StackLayout;
                        var employeeGrid = Lablestacklayout.Children[0] as Grid;

                        //var employeecontrcatorids1 = employeeGrid.Children[0] as Label;
                        // var employeecontrcatoridssubstring = employeecontrcatorids.StyleId.Split(':');
                        // string EmpCntID = Int32.Parse(employeecontrcatoridssubstring[0]).ToString();
                        // string EmpCntValue = employeecontrcatoridssubstring[1];

                        employeecontrcatorids = employeeGrid.Children[1] as Label;
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
                        if (HoursTextType == false && MinTextType == false)
                        {
                            listtoAnswer.Add(new InspectionTOAnswers()
                            {
                                WorkOrderID = WorkorderID,
                                InspectionTime = ((int)totalTime).ToString(),
                                StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                EmployeeLaborCraftid = EmpLaborCraftID,
                                ContractorLaborCraftId = ContLaborCraftID,
                                ModifiedUserName = AppSettings.UserName,
                                WorkOrderInspectionTimeID = WorkOrderInspectionTimeIDs,
                                IsManual = true,
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

                    
                }


                UserDialogs.Instance.HideLoading();
                total = total.Subtract(total);
                MainLayout.Children.Clear();
                ParentLayout.Children.Clear();
                OnAppearing();

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

            }

        }
        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
            await Task.Delay(1000);
            CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
            if (CC.workOrderEmployee.Count == 0 && CC.workorderContractor.Count == 0)
            {

                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("PleaseFirstAddEmployeeORContractor"), TimeSpan.FromSeconds(2));
                UserDialogs.Instance.HideLoading();
                return;

            }
            List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();
            List<InspectionTOAnswers> liststartAnswer = new List<InspectionTOAnswers>();
            List<InspectionTOAnswers> listcompletionAnswer = new List<InspectionTOAnswers>();

            InspectionTOAnswers listtoAnswerModel;


            var test = (sender as Button).Parent.Parent.Parent.Parent;
            var ssdv = (sender as Button).Parent.Parent.Parent as StackLayout;

            var kfm1 = ssdv.Children[0] as StackLayout;
            var kfm = kfm1.Children[0] as StackLayout;

            //  var kfm1 = ParentLayout.Children[0] as StackLayout;

            if (kfm == null)
            {
                return;
            }
            var gridPF = kfm.Children[0] as Grid;
            var context = gridPF.BindingContext as ExistingInspections;

            //var context = kfm.BindingContext as ExistingInspections;
            string value = null;

            switch (context.ResponseTypeName)
            {

                case "Pass/Fail":
                    var btn1PF = gridPF.Children[2] as SfButton;
                    var btn2PF = gridPF.Children[3] as SfButton;
                    value = "";
                    if (btn1PF.BackgroundColor != Color.LightGray)
                    {
                        value = btn1PF.Text;
                    }
                    if (btn2PF.BackgroundColor != Color.LightGray)
                    {
                        value = btn2PF.Text;
                    }
                    break;

                case "Standard Range":
                    var SRangeBorder = gridPF.Children[2] as SfBorder;
                    var dataTextRange = SRangeBorder.Content as Entry;
                    value = dataTextRange.Text;
                    break;

                case "Yes/No/N/A":
                    var btn1 = gridPF.Children[2] as SfButton;
                    var btn2 = gridPF.Children[3] as SfButton;
                    var btn3 = gridPF.Children[4] as SfButton;
                    value = "";
                    if (btn1.BackgroundColor != Color.LightGray)
                    {
                        value = btn1.Text;
                    }
                    if (btn2.BackgroundColor != Color.LightGray)
                    {
                        value = btn2.Text;
                    }

                    //TODO: add the btn3 which is NA button and add the value.
                    if (btn3.BackgroundColor != Color.LightGray)
                    {
                        value = btn3.Text;
                    }

                    break;


                case "Count":
                    var data = (gridPF.Children[2] as MyEntry) as Entry;
                    value = data.Text;
                    break;
                case "Text":
                    var textEntity = gridPF.Children[2] as SfBorder;
                    var dataText = textEntity.Content as Editor;
                    value = dataText.Text;
                    break;

                case "Multiple Choice":
                    var index3 = (gridPF.Children[2] as Picker).SelectedIndex;
                    if (index3 == -1)
                    {
                        value = null;
                    }
                    else
                    {
                        value = (gridPF.Children[2] as Picker).Items[index3];
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
                var data = (sender as Button).Parent;
                signatureImageView = ((sender as Button).Parent as Grid).Children[0] as CustomImage;
                //signatureImageView = ((sender as SfButton).Parent as Grid).Children[2] as CustomImage;
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

            var grid = ParentLayout.Children[0] as Grid;
            var stacklayout = grid.Children[2] as StackLayout;
            var SVLayout = stacklayout.Children[0] as ScrollView;
            var stacklayout1 = SVLayout.Children[0] as StackLayout;


            try
            {
                for (int i = 0; i < stacklayout1.Children.Count; i++)
                {
                    string EmpLaborCraftID = "0";
                    string ContLaborCraftID = "0";
                    int? WorkOrderInspectionTimeIDs = null;
                    listtoAnswerModel = new InspectionTOAnswers();
                    var FrameLayout = stacklayout1.Children[i] as Frame;
                    if (!string.IsNullOrWhiteSpace(FrameLayout.StyleId))
                    {
                        WorkOrderInspectionTimeIDs = Convert.ToInt32(FrameLayout.StyleId);
                    }
                    var Fstacklayout = FrameLayout.Children[0] as StackLayout;
                    var Fstacklayout1 = Fstacklayout.Children[0] as StackLayout;

                    var Fstacklayout2 = Fstacklayout1.Children[0] as Grid;

                    var Hrsstacklayout3 = Fstacklayout2.Children[3] as StackLayout;
                    var HrsGrid = Hrsstacklayout3.Children[0] as Grid;
                    var Hrsgridlayout = HrsGrid.Children[0] as SfBorder;
                    var HoursEntryValue = Hrsgridlayout.Children[0] as Entry;
                    var HoursTextType = HoursEntryValue.IsReadOnly;
                    var HoursEntryValue1 = HoursEntryValue.Text;

                    var Mistacklayout3 = Fstacklayout2.Children[4] as StackLayout;
                    var MiGrid = Mistacklayout3.Children[0] as Grid;
                    var Migridlayout = MiGrid.Children[0] as SfBorder;
                    var MinuteEntryValue = Migridlayout.Children[0] as Entry;
                    var MinTextType = HoursEntryValue.IsReadOnly;
                    var MinuteEntryValue1 = MinuteEntryValue.Text;

                    var Datelayout1 = Fstacklayout.Children[1] as StackLayout;
                    var DateGrid1 = Datelayout1.Children[0] as Grid;
                    var DateSLt1 = DateGrid1.Children[0] as StackLayout;
                    var DateBodder1 = DateSLt1.Children[1] as SfBorder;
                    var StartdateValue = DateBodder1.Children[0] as CustomDatePicker1;
                    //var StartdateValue1 = DateTime.Parse(StartdateValue.SelectedDate.ToString());

                    var DateSLt2 = DateGrid1.Children[1] as StackLayout;
                    var DateBodder2 = DateSLt2.Children[1] as SfBorder;
                    var CompletionDateValue = DateBodder2.Children[0] as CustomDatePicker1;
                    // var CompletionDateValue1 = DateTime.Parse(CompletionDateValue.SelectedDate.ToString());

                    if (StartdateValue != null)
                    {
                        this.InspectionStartDate = StartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(StartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    }
                    else
                    {
                        this.InspectionStartDate = null;
                    }
                    if (CompletionDateValue != null)
                    {
                        this.InspectionCompletionDate = CompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(CompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    }
                    else
                    {
                        this.InspectionCompletionDate = null;
                    }


                    var Lablestacklayout = Fstacklayout2.Children[0] as StackLayout;
                    var employeeGrid = Lablestacklayout.Children[0] as Grid;

                    //var employeecontrcatorids1 = employeeGrid.Children[0] as Label;
                    // var employeecontrcatoridssubstring = employeecontrcatorids.StyleId.Split(':');
                    // string EmpCntID = Int32.Parse(employeecontrcatoridssubstring[0]).ToString();
                    // string EmpCntValue = employeecontrcatoridssubstring[1];

                    employeecontrcatorids = employeeGrid.Children[1] as Label;
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
                    // Foe Frame
                    //listtoAnswerModel.WorkOrderID = WorkorderID;
                    //    listtoAnswerModel.InspectionTime = ((int)totalTime).ToString();
                    //listtoAnswerModel.StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null;
                    //listtoAnswerModel.CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null;
                    //listtoAnswerModel.EmployeeLaborCraftid = EmpLaborCraftID;
                    //listtoAnswerModel.ContractorLaborCraftId = ContLaborCraftID;
                    //listtoAnswerModel.ModifiedUserName = AppSettings.UserName;
                    //listtoAnswer.Add(listtoAnswerModel);

                    if (MinTextType == false && HoursTextType == false)
                    {
                        listtoAnswer.Add(new InspectionTOAnswers()
                        {
                            WorkOrderID = WorkorderID,
                            InspectionTime = ((int)totalTime).ToString(),
                            StartDate = this.InspectionStartDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null,
                            CompletionDate = this.InspectionCompletionDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null,
                            EmployeeLaborCraftid = EmpLaborCraftID,
                            ContractorLaborCraftId = ContLaborCraftID,
                            ModifiedUserName = AppSettings.UserName,
                            WorkOrderInspectionTimeID = WorkOrderInspectionTimeIDs
                        });

                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = this.InspectionStartDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null,

                        });
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {

                            CompletionDate = this.InspectionCompletionDate.HasValue ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone) : (DateTime?)null,

                        });
                    }

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

                //foreach (var item in workOrderInspectionTimeID)
                //{
                //    string Employeekey = "WorkOrderEmployee:" + item.Value;
                //    string Contractorkey = "WorkorderContracator:" + item.Value;
                //    WorkorderInspectionStorge.Storage.Delete(Employeekey);
                //    WorkorderInspectionStorge.Storage.Delete(Contractorkey);

                //}
                // workOrderInspectionTimeID.Clear();

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
            ParentLayout.Children.Clear();
            OnAppearing();


        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {

            var ssdv = (sender as Button).Parent as Grid;

            Button kfm;
            if (ssdv.Children.Count == 5)
            {
                kfm = ssdv.Children[2] as Button;
            }
            else
            {
                kfm = ssdv.Children[1] as Button;
            }

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

            var ssdv = (sender as Button).Parent as Grid;
            Button kfm;
            if (ssdv.Children.Count == 5)
            {
                kfm = ssdv.Children[2] as Button;
            }
            else
            {
                kfm = ssdv.Children[1] as Button;
            }

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

            var EmployeeID = (sender as SfButton);


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

            var ContrcatorID = (sender as SfButton);


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
        //private void DeleteSavedTimer()
        //{
        //    //this.TimerText.Text = "";
        //    string key = "WorkOrderInspection:" + WorkorderID;
        //    WorkorderInspectionStorge.Storage.Delete(key);
        //}
        private string ExtractValueFormSection(StackLayout kfm)
        {
            var LalGrid = kfm.Children[0] as StackLayout;
            var LalGrid1 = LalGrid.Children[0] as Grid;
            var context = (LalGrid1.Children[1] as Label).BindingContext as ExistingInspections;

            string value = null;

            switch (context.ResponseTypeName)
            {
                case "Pass/Fail":

                    var gridPF = LalGrid.Children[0] as Grid;
                    var btn1PF = gridPF.Children[2] as SfButton;
                    var btn2PF = gridPF.Children[3] as SfButton;
                    value = "";
                    if (btn1PF.BackgroundColor != Color.LightGray)
                    {
                        value = btn1PF.Text;
                    }
                    if (btn2PF.BackgroundColor != Color.LightGray)
                    {
                        value = btn2PF.Text;
                    }
                    break;
                //value = (kfm.Children[2] as Button).Text;
                //break;

                case "Standard Range":

                    var gridSRange = LalGrid.Children[0] as Grid;
                    var SRangeBorder = gridSRange.Children[2] as SfBorder;
                    var dataTextRange = SRangeBorder.Content as Entry;
                    value = dataTextRange.Text;
                    break;

                case "Yes/No/N/A":
                    //value = (kfm.Children[2] as Button).Text;

                    var grid = LalGrid.Children[0] as Grid;
                    var btn1 = grid.Children[2] as SfButton;
                    var btn2 = grid.Children[3] as SfButton;
                    var btn3 = grid.Children[4] as SfButton;
                    value = "";
                    if (btn1.BackgroundColor != Color.LightGray)
                    {
                        value = btn1.Text;
                    }
                    if (btn2.BackgroundColor != Color.LightGray)
                    {
                        value = btn2.Text;
                    }

                    //TODO: add the btn3 which is NA button and add the value.
                    if (btn3.BackgroundColor != Color.LightGray)
                    {
                        value = btn3.Text;
                    }

                    break;


                case "Count":
                    var gridCount = LalGrid.Children[0] as Grid;
                    var data = (gridCount.Children[2] as MyEntry) as Entry;
                    value = data.Text;
                    break;
                case "Text":
                    var gridText = LalGrid.Children[0] as Grid;
                    var textEntity = gridText.Children[2] as SfBorder;
                    var dataText = textEntity.Content as Editor;
                    value = dataText.Text;
                    break;

                case "Multiple Choice":
                    var gridMChoice = LalGrid.Children[0] as Grid;
                    var index3 = (gridMChoice.Children[2] as Picker).SelectedIndex;
                    if (index3 == -1)
                    {
                        value = null;
                    }
                    else
                    {
                        value = (gridMChoice.Children[2] as Picker).Items[index3];
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
            //if (ParentLayout.Children.Count > 5)
            //{
            //    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("CannotAddMorethan5EmployeeORContractor"), TimeSpan.FromSeconds(2));
            //    return;
            //}
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
            //if (ParentLayout.Children.Count > 5)
            //{
            //    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("CannotAddMorethan5EmployeeORContractor"), TimeSpan.FromSeconds(2));
            //    return;
            //}
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

