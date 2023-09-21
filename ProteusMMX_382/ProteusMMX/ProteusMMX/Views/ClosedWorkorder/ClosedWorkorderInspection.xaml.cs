using Acr.UserDialogs;
using Newtonsoft.Json;
using NodaTime;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.ViewModel.ClosedWorkorder;
using ProteusMMX.ViewModel.Miscellaneous;
using SignaturePad.Forms;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.Expander;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClosedWorkorderInspection : ContentPage
    {
        TimeSpan total;
        Label TotalInspectionTime;
        Label TotalInspectionTimeHours;
        Label TotalInspectionTimeMinutes;
        StackLayout layout1 = new StackLayout();
        double totalTime;
        ServiceOutput Closedworkorder;
        public int? ClosedWorkorderID { get; set; }
        public string BaseURL { get; set; }

        ServiceOutput ClosedWorkorderInspectionList;

        public StringBuilder AnswerText = new StringBuilder();

        string FinalHours;

        public string UserId = AppSettings.User.UserID.ToString();
        string ServerTimeZone = AppSettings.User.ServerIANATimeZone;
        string UserTimeZone = AppSettings.ClientIANATimeZone;

        public ServiceOutput inspectionTime { get; set; }
        public DateTime? InspectionCompletionDate { get; set; }
        public DateTime? InspectionStartDate { get; set; }

        public ClosedWorkorderInspection()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            // this.Title = WebControlTitle.GetTargetNameByTitleName("Inspection");
        }

        ClosedWorkorderInspectionViewModel ViewModel => this.BindingContext as ClosedWorkorderInspectionViewModel;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                await viewAware.OnViewAppearingAsync(this);
            }
            await OnAppearingOld();
        }


        protected async Task OnAppearingOld()
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("TaskOrInspection"))
                {
                    string TaskorInspection = (string)Application.Current.Properties["TaskOrInspection"];
                    if (TaskorInspection == "Task")
                    {
                        ViewModel.DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                        ViewModel.DisabledTextIsEnable = true;
                        return;
                    }
                }
                this.ClosedWorkorderID = ViewModel.ClosedWorkorderID;
                AnswerText.Clear();
                ParentLayout.Children.Clear();
                MainLayout.Children.Clear();
                await RetriveAllWorkorderInspectionsAsync();
            }
            catch (Exception ex)
            {

                // Debug.WriteLine(ex.Message + "<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>" + ex.StackTrace);
            }

        }


        private async Task RetriveAllWorkorderInspectionsAsync()
        {

            ClosedWorkorderInspectionList = await ViewModel._inspectionService.GetClosedWorkOrdersInspection(this.ClosedWorkorderID.ToString(), this.UserId);



            Grid masterGrid = new Grid();
            masterGrid.BackgroundColor = Color.White;
            masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            // masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            masterGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            StackLayout TotalInspectionSL = new StackLayout();
            TotalInspectionSL.Padding = new Thickness(5, 2, 0, 0);
            masterGrid.Children.Add(TotalInspectionSL, 0, 0);

            TotalInspectionTime = new Label();
            TotalInspectionTimeHours = new Label();
            TotalInspectionTimeMinutes = new Label();

            StackLayout FrameSL = new StackLayout
            {
                Padding = new Thickness(3, 0, 3, 0)
            };

            masterGrid.Children.Add(FrameSL, 0, 1);
            ScrollView FrameSV = new ScrollView();
            // masterGrid.Children.Add(FrameSV, 0, 1);
            FrameSL.Children.Add(FrameSV);
            StackLayout FramesSL = new StackLayout();
            FrameSV.Content = FramesSL;

            //TotalInspectionTime = new Label();
            if (ClosedWorkorderInspectionList.clWorkOrderWrapper == null || ClosedWorkorderInspectionList.clWorkOrderWrapper.ClosedWorkOrderInspection.Count == 0)
            {

                // this.InspectionTimerLayout.IsVisible = false;
                return;
            }
            int CCCount = ClosedWorkorderInspectionList.workOrderEmployee.Count + ClosedWorkorderInspectionList.workorderContractor.Count;

            if (CCCount > 1)
            {
                MasterMainGrid.RowDefinitions[0].Height = GridLength.Star;
            }

            foreach (var item in ClosedWorkorderInspectionList.workOrderEmployee)
            {
                Frame Associateframe = new Frame
                {
                    CornerRadius = 5,
                    BorderColor = Color.Black
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
                    CommandParameter = item,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/Cstart.png" : "Cstart.jpg",
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
                SfButton stopButton = new SfButton
                {
                    FontSize = 12,
                    BorderWidth = 2,
                    WidthRequest = 85,
                    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                    FontAttributes = FontAttributes.Bold,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/Cstop.png" : "Cstop.jpg" ,
                    CommandParameter = item,
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
                    HeightRequest = 35,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10,
                    IsEnabled = false,
                };
                HrsGrid.Children.Add(HrsBorder);
                Entry hoursEntry = new Entry
                {
                    HeightRequest = 35,
                    Placeholder = "Hrs",
                    FontSize = 12,
                    BackgroundColor = Color.LightGray,
                    TextColor = Color.Black,
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
                    HeightRequest = 35,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10,
                    IsEnabled = false,
                };
                MinGrid.Children.Add(MinBorder);
                Entry minuteEntry = new Entry
                {
                    HeightRequest = 35,
                    Placeholder = "Min",
                    FontSize = 12,
                    BackgroundColor = Color.LightGray,
                    TextColor = Color.Black,
                };
                MinBorder.Content = minuteEntry;

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
                    BackgroundColor = Color.LightGray,
                    CornerRadius = 10,
                   
                };
                FromDateSL.Children.Add(dateFromBo);
                Label startDate;

                if (item.StartDate != null)
                {
                    string sdate = Convert.ToString(DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("MMM d, yyyy hh:mm tt"));
                    startDate = new Label
                    {
                        Margin = new Thickness(0, 5, 0, 0),
                        Text = sdate,
                       // MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        BackgroundColor = Color.LightGray,
                        TextColor=Color.Black,
                    };
                    dateFromBo.Content = startDate;
                }
                else
                {
                    startDate = new Label
                    {
                        Margin = new Thickness(0, 5, 0, 0),
                        Text = "",
                        //MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black,
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
                    CornerRadius = 10,
                    BackgroundColor = Color.LightGray,
                    
                };
                CompDateSL.Children.Add(dateCompBo);
                Label CompletionDate;
                if (item.CompletionDate != null)
                {
                    CompletionDate = new Label
                    {
                        Text =Convert.ToString(DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("MMM d, yyyy hh:mm tt")),                        
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 0, 0),
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black,
                        
                    };
                    dateCompBo.Content = CompletionDate;
                }

                else
                {
                    CompletionDate = new Label
                    {
                        //MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        Text = "",
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 0, 0),
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black,
                        
                    };
                    dateCompBo.Content = CompletionDate;
                }
                #endregion


                if (!string.IsNullOrWhiteSpace(item.InspectionTime))
                {
                    try
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
                    catch (Exception)
                    {

                        throw;
                    }
                }

            }

            foreach (var item in ClosedWorkorderInspectionList.workorderContractor)
            {

                Frame Associateframe = new Frame
                {
                    CornerRadius = 5,
                    BorderColor = Color.Black
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
                    Text = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": " + item.ContractorName + "(" + item.LaborCraftCode + ")",
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#006de0"),
                    Margin = new Thickness(0, 0, 0, 0),
                };


                AssociateGrid.Children.Add(AssociateLal, 0, 0);
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
                    CommandParameter = item,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/Cstart.png" : "Cstart.jpg",
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
                SfButton stopButton = new SfButton
                {
                    FontSize = 12,
                    BorderWidth = 2,
                    WidthRequest = 85,
                    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                    FontAttributes = FontAttributes.Bold,
                    ShowIcon = true,
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    ImageSource = (Device.RuntimePlatform == Device.UWP) ? "Assets/Cstop.png" : "Cstop.jpg" ,
                    CommandParameter = item,
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
                    HeightRequest = 35,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10,
                    IsEnabled = false,
                };
                HrsGrid.Children.Add(HrsBorder);
                Entry hoursEntry = new Entry
                {
                    HeightRequest = 35,
                    Placeholder = "Hrs",
                    FontSize = 12,
                    BackgroundColor = Color.LightGray,
                    TextColor = Color.Black,
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
                    HeightRequest = 35,
                    WidthRequest = 50,
                    BorderColor = Color.Black,
                    CornerRadius = 10,
                    IsEnabled = false,
                };
                MinGrid.Children.Add(MinBorder);
                Entry minuteEntry = new Entry
                {
                    HeightRequest = 35,
                    Placeholder = "Min",
                    FontSize = 12,
                    BackgroundColor = Color.LightGray,
                    TextColor = Color.Black,
                };
                MinBorder.Content = minuteEntry;

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
                    CornerRadius = 10,
                    BackgroundColor = Color.LightGray,
                   
                };
                FromDateSL.Children.Add(dateFromBo);


                Label startDate;
                if (item.StartDate != null)
                {
                    startDate = new Label
                    {
                        Margin = new Thickness(0, 5, 0, 0),
                        Text = Convert.ToString(DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("MMM d, yyyy hh:mm tt")),
                        //MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                        HeightRequest = 2,
                        BackgroundColor = Color.LightGray,
                        TextColor=Color.Black,
                    };
                    dateFromBo.Content = startDate;
                }
                else
                {
                    startDate = new Label
                    {
                        Margin = new Thickness(0, 5, 0, 0),
                        // MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),                       
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black,
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
                    CornerRadius = 10,
                    BackgroundColor = Color.LightGray,
                   
                };
                CompDateSL.Children.Add(dateCompBo);
                Label CompletionDate;
                if (item.CompletionDate != null)
                {
                    CompletionDate = new Label
                    {
                        Text = Convert.ToString(DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("MMM d, yyyy hh:mm tt")),
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 0, 0),
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black,
                    };
                    dateCompBo.Content = CompletionDate;
                }

                else
                {
                    CompletionDate = new Label
                    {
                        // MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),                        
                        HeightRequest = 2,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 0, 0),
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black,
                    };
                    dateCompBo.Content = CompletionDate;
                }
                #endregion

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

            Label TotalInspectionLal = new Label
            {
                Text = WebControlTitle.GetTargetNameByTitleName("TotalInspectionTime") + "(HH:MM) " + ":  " + TotalInspectionTime.Text,
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
            BindLayout(ClosedWorkorderInspectionList.clWorkOrderWrapper.ClosedWorkOrderInspection);


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
                    //this.PickerInspectionStartDate.Text = context.InspectionStartTime.GetValueOrDefault().Date.ToString();
                    //this.InspectionStartDate = context.InspectionStartTime.GetValueOrDefault().Date;

                    //this.PickerInspectionCompletionDate.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToString();
                    //this.InspectionCompletionDate = context.InspectionStopTime.GetValueOrDefault().Date;
                }

                else
                {
                    //this.PickerInspectionStartDate.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                    //this.InspectionStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).Date;

                    //this.PickerInspectionCompletionDate.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToString();
                    //this.InspectionCompletionDate = context.InspectionStopTime.GetValueOrDefault().Date;
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
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("WronginputinMinutes."), TimeSpan.FromSeconds(2));

                e1.Text = "";
                return;
            }
            if (minuteValue > 59)
            {
                //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Minutesshouldbelessthen59."), WebControlTitle.GetTargetNameByTitleName("OK"));

                //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Minutesshouldbelessthen59").TargetName, flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Minutesshouldbelessthen59."), TimeSpan.FromSeconds(2));

                e1.Text = "";
                return;
            }

        }
        private void BindLayout(List<ClosedWorkOrderInspection> listInspection)
        {
            List<ClosedWorkOrderInspection> distinctList = listInspection.OrderByDescending(x => string.IsNullOrEmpty(x.SectionName)).ToList();

            #region ***** Next *****

            StackLayout TabViewSL = new StackLayout();
            // masterGrid.Children.Add(TabViewSL, 0, 4);
            #region **** MiscellaneousGrid ****

            Grid MiscellaneousGrid = new Grid { BackgroundColor = Color.White, MinimumWidthRequest = 300, Padding = 2 };
            //  Grid MiscellaneousGrid = new Grid { BackgroundColor = Color.White,  Padding = 2 };
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

            Grid GroupSectionsGrid = new Grid { BackgroundColor = Color.White, HeightRequest = 300, Padding = 2 };
            //Grid GroupSectionsGrid = new Grid { BackgroundColor = Color.White, Padding = 2 };
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

            int Misce = 0;
            int GroupSec = 0;
            #endregion

            foreach (var item in distinctList)
            {
                StackLayout layout2 = new StackLayout();


                if (string.IsNullOrEmpty(item.SectionName))
                {
                    Misce = Misce + 1;
                    View Question;
                    View Label;
                    //View LabelHours;
                    View Layout;

                    Grid sta;
                    switch (item.ResponseType)
                    {
                        #region ***** Yes No ****

                        case "Pass/Fail":
                            StackLayout PassFailSl = new StackLayout();
                            //StackLayout PassFailSlLavel = new StackLayout();
                            StackLayout PassFailSlButton = new StackLayout();

                            var PassFailgrid = new Grid() { HorizontalOptions = LayoutOptions.End, BindingContext = item };
                            PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                            PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                            PassFailSlButton.Children.Add(PassFailgrid);

                            PassFailSl.Children.Add(PassFailSlButton);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, };
                            var btnTruePF = new SfButton()
                            {
                                Text = "Pass",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 36
                            };
                            var btnFalsePF = new SfButton()
                            {
                                Text = "Fail",
                                TextColor = Color.Black,
                                FontSize = 11,
                                BackgroundColor = Color.LightGray,
                                FontAttributes = FontAttributes.Bold,
                                CornerRadius = 70,
                                HeightRequest = 36
                            };
                            //btnTruePF.Clicked += BtnTrue_Clicked;
                            //btnFalsePF.Clicked += BtnFalse_Clicked;
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
                                    BtnFalse_Clicked(btnFalsePF, null);
                                    break;
                            }
                            break;

                        #endregion
                        #region ***** Standard Range ****

                        case "Standard Range":
                            StackLayout SRangeSl = new StackLayout();
                            StackLayout SRangeSlLavel = new StackLayout();
                            var SRangegrid = new Grid() { BindingContext = item };
                            SRangegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            SRangegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                            SRangeSlLavel.IsEnabled = false;
                            SRangeSlLavel.Children.Add(SRangegrid);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            Layout = new Entry() { Keyboard = Keyboard.Numeric, BackgroundColor = Color.LightGray };
                            SRangeSl.Children.Add(SRangeSlLavel);
                            GenerateAnswerText(item);
                            SfBorder RangeBor = new SfBorder() { CornerRadius = 5 };
                            RangeBor.IsEnabled = false;
                            (Layout as Entry).BindingContext = new Range() { MaxRange = item.MaxRange, MinRange = item.MinRange };
                            (Layout as Entry).BackgroundColor = Color.LightGray;
                            RangeBor.Content = Layout;
                            SRangegrid.Children.Add(Question, 0, 0);
                            SRangegrid.Children.Add(Label, 0, 0);
                            SRangegrid.Children.Add(RangeBor, 1, 0);
                            (Layout as Entry).TextChanged += StandardRange_TextChanged;
                            (Layout as Entry).Text = item.AnswerDescription;
                            (Layout as Entry).BackgroundColor = Color.LightGray;
                            layout2.Children.Add(SRangeSl);
                            if (!string.IsNullOrWhiteSpace(item.AnswerDescription))
                            {
                                if (Convert.ToDecimal(item.AnswerDescription) >= item.MinRange && Convert.ToDecimal(item.AnswerDescription) <= item.MaxRange)
                                {
                                    //this.CreateWorkorderButtonSL.IsVisible = false;
                                }
                                else
                                {
                                    //this.CreateWorkorderButtonSL.IsVisible = true;
                                }
                            }
                            break;

                        #endregion
                        #region ****** Yes/No/N/A *****

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

                            YesNoSl.Children.Add(YesNoSlButton);
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black };

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
                            //btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                            //btnFalse.Clicked += BtnNo_Clicked;
                            //btnNA.Clicked += BtnNA_Clicked;
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
                                        // CreateWorkorderButtonSL.IsVisible = true;
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
                                        //CreateWorkorderButtonSL.IsVisible = true;
                                        //var MGrid = this.CreateWorkorderButtonSL.Parent as Grid;
                                        // MGrid.Children[3].IsVisible = true;
                                        break;

                                }
                            }
                            #endregion


                            break;

                        #endregion
                        case "Count":
                            StackLayout CountSl = new StackLayout();
                            StackLayout CountSlLavel = new StackLayout();
                            Grid CountGrid = new Grid() { BindingContext = item };

                            CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 100 });
                            CountSlLavel.IsEnabled = false;
                            CountSlLavel.Children.Add(CountGrid);

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };
                            GenerateAnswerText(item);
                            SfBorder CountBor = new SfBorder() { CornerRadius = 5 };

                            Layout = new Entry() { Keyboard = Keyboard.Numeric, BackgroundColor = Color.LightGray, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };
                            (Layout as Entry).Text = item.AnswerDescription;
                            (Layout as Entry).TextChanged += OnlyNumeric_TextChanged;
                            (Layout as Entry).BackgroundColor = Color.LightGray;
                            CountBor.Content = Layout;
                            CountGrid.Children.Add(Question, 0, 0);
                            CountGrid.Children.Add(Label, 0, 0);
                            CountGrid.Children.Add(CountBor, 1, 0);
                            CountSl.Children.Add(CountSlLavel);
                            layout2.Children.Add(CountSl);

                            break;
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

                            Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.LightGray, HeightRequest = 60 };
                            (Layout as CustomEditor).Text = item.AnswerDescription;
                            SfBorder TextSfBorder = new SfBorder
                            {
                                BorderColor = Color.Black,
                                BorderWidth = 1,
                                CornerRadius = 5,
                                IsEnabled = false
                            };
                            TextSfBorder.Content = Layout;
                            Textgrid.Children.Add(Question, 0, 0);
                            Textgrid.Children.Add(Label, 0, 0);
                            Textgrid.Children.Add(TextSfBorder, 0, 1);

                            TextSl.Children.Add(TextSlLavel);

                            layout2.Children.Add(TextSl);

                            break;

                        case "Multiple Choice":
                            StackLayout MChoiceSl = new StackLayout();
                            StackLayout MChoiceSlLavel = new StackLayout();
                            MChoiceSlLavel.IsEnabled = false;
                            MChoiceSl.Children.Add(MChoiceSlLavel);
                            Grid MChoiceGrid = new Grid() { BindingContext = item };
                            MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            MChoiceSlLavel.Children.Add(MChoiceGrid);
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);

                            MChoiceSlLavel.Children.Add(Label);
                            Layout = new CustomPicker() { VerticalOptions = LayoutOptions.Start, BackgroundColor = Color.LightGray, HorizontalOptions = LayoutOptions.End, Image = "unnamed" };

                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);


                            //  Layout = new Picker() {Title=item.AnswerDescription, IsEnabled = false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };

                            Label Label1 = new Label { Text = item.AnswerDescription, BackgroundColor = Color.LightGray, HorizontalOptions = LayoutOptions.End };

                            MChoiceGrid.Children.Add(Question, 0, 0);
                            MChoiceGrid.Children.Add(Label, 0, 0);
                            MChoiceGrid.Children.Add(Label1, 1, 0);

                            MChoiceSlLavel.Children.Add(MChoiceGrid);
                            MChoiceSl.Children.Add(MChoiceSlLavel);
                            layout2.Children.Add(MChoiceSl);

                            break;

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
                    }

                    #region ***** Buttons ***

                    StackLayout Case1SL = new StackLayout();
                    layout2.Children.Add(Case1SL);
                    Grid Case1Grid = new Grid();
                    Case1Grid.Padding = new Thickness(0, 0, 0, 0);
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

                        if (!string.IsNullOrWhiteSpace(item.SignatureString))
                        {
                            var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 0 };
                            byte[] byteImg = Convert.FromBase64String(item.SignatureString);
                            imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                            Case1Grid.Children.Add(imageView, 0, 0);
                            Grid.SetColumnSpan(imageView, 5);
                            if (byteImg.Length > 0)
                            {
                                imageView.HeightRequest = 150;
                                imageView.BackgroundColor = Color.LightGray;
                            }

                            var s = item.SignaturePath;

                        }
                        else
                        {

                        }
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




                    #region Estimated Hour Region
                    //var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    //var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                    //var estimatedHourLabel = new Label() { TextColor = Color.Black };
                    //estimatedHourLabel.Text = item.EstimatedHours.ToString();
                    //estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                    //estimatedHourStackLayout.Children.Add(estimatedHourLabel);

                    //mainLayoutGroup.Children.Add(estimatedHourStackLayout);
                    #endregion

                    //var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                    //mainLayoutGroup.Children.Add(oneBox);

                    //MainLayout.Children.Add(mainLayoutGroup);

                    //this.AnswerText.Append(System.Environment.NewLine);
                    layout2Test.Children.Add(layout2);
                }

                else
                {
                    List<ClosedWorkOrderInspection> commonSections = new List<ClosedWorkOrderInspection>();

                    foreach (var item1 in listInspection)
                    {
                        if (item1.IsAdded == false)
                        {
                            if (item.SectionName == item1.SectionName)
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

                    foreach (var item1 in commonSections)
                    {
                        View Question;
                        View Label;
                        View Layout;

                        //StackLayout sta;
                        Grid sta;

                        switch (item1.ResponseType)
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
                                    FontSize = 11,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 36
                                };
                                var btnFalsePF = new SfButton()
                                {
                                    Text = "Fail",
                                    TextColor = Color.Black,
                                    FontSize = 11,
                                    BackgroundColor = Color.LightGray,
                                    FontAttributes = FontAttributes.Bold,
                                    CornerRadius = 70,
                                    HeightRequest = 36
                                };


                                // btnTruePF.Clicked += BtnTrue_Clicked;
                                // btnFalsePF.Clicked += BtnFalse_Clicked;
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
                                        // this.CreateWorkorderButtonSL.IsVisible = true;
                                        BtnFalse_Clicked(btnFalsePF, null);
                                        break;

                                }
                                break;

                            case "Standard Range":
                                StackLayout SRangeSl = new StackLayout();
                                StackLayout SRangeSlButton = new StackLayout();
                                SRangeSl.Children.Add(SRangeSlButton);
                                Grid sta1 = new Grid() { BindingContext = item };
                                sta1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta1.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                sta1.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                                SRangeSlButton.Children.Add(sta1);

                                var Questions = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                var Label1s = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                SfBorder RangeBor = new SfBorder() { CornerRadius = 5, IsEnabled = false };

                                Entry Layouts = new Entry() { Keyboard = Keyboard.Numeric, BackgroundColor = Color.LightGray, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };

                                Layouts.BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                                RangeBor.Content = Layouts;
                                sta1.Children.Add(Questions, 0, 0);
                                sta1.Children.Add(Label1s, 0, 0);
                                sta1.Children.Add(RangeBor, 1, 0);
                                Layouts.TextChanged += StandardRange_TextChanged;
                                Layouts.Text = item1.AnswerDescription;
                                Layouts.BackgroundColor = Color.LightGray;
                                ItemListCase1Sl.Children.Add(SRangeSl);
                                if (!string.IsNullOrWhiteSpace(item1.AnswerDescription))
                                {
                                    if (Convert.ToDecimal(item1.AnswerDescription) >= item1.MinRange && Convert.ToDecimal(item1.AnswerDescription) <= item1.MaxRange)
                                    {
                                        //this.CreateWorkorderButtonSL.IsVisible = false;
                                    }
                                    else
                                    {
                                        //this.CreateWorkorderButtonSL.IsVisible = true;
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
                                //btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                //btnFalse.Clicked += BtnNo_Clicked;
                                //btnNA.Clicked += BtnNA_Clicked;
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
                                            //CreateWorkorderButtonSL.IsVisible = true;
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
                                            // CreateWorkorderButtonSL.IsVisible = true;
                                            // var MGrid = this.CreateWorkorderButtonSL.Parent as Grid;
                                            // MGrid.Children[3].IsVisible = true;
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
                                CountSlLavel.IsEnabled = false;
                                CountSlLavel.Children.Add(CountGrid);

                                var Questionc = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                var Label1c = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };


                                GenerateAnswerText(item1);

                                Entry Layoutc = new Entry() { Keyboard = Keyboard.Numeric, BackgroundColor = Color.LightGray, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };
                                SfBorder CountBor = new SfBorder { CornerRadius = 5 };

                                Layoutc.Text = item1.AnswerDescription;
                                Layoutc.TextChanged += OnlyNumeric_TextChanged;
                                Layoutc.BackgroundColor = Color.LightGray;
                                CountBor.Content = Layoutc;
                                CountGrid.Children.Add(Questionc, 0, 0);
                                CountGrid.Children.Add(Label1c, 0, 0);
                                CountGrid.Children.Add(CountBor, 1, 0);

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

                                var Layoutt = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.LightGray, HeightRequest = 60 };
                                (Layoutt as CustomEditor).Text = item1.AnswerDescription;

                                SfBorder TextSfBorder = new SfBorder
                                {
                                    BorderColor = Color.Black,
                                    BorderWidth = 1,
                                    CornerRadius = 5,
                                    IsEnabled = false
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

                                //Layout = new Picker() {Title=item.AnswerDescription, IsEnabled = false, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
                                Label Label2 = new Label { Text = item1.AnswerDescription, BackgroundColor = Color.LightGray, HorizontalOptions = LayoutOptions.End };

                                MChoiceGrid.Children.Add(Questionm, 0, 0);
                                MChoiceGrid.Children.Add(Label1m, 0, 0);
                                MChoiceGrid.Children.Add(Label2, 1, 0);
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


                    StackLayout Case1SL = new StackLayout();
                    layout2.Children.Add(Case1SL);
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

                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };

                        if (!string.IsNullOrWhiteSpace(item.SignatureString))
                        {
                            var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 0 };
                            byte[] byteImg = Convert.FromBase64String(item.SignatureString);
                            imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                            Case1Grid.Children.Add(imageView, 0, 0);
                            Grid.SetColumnSpan(imageView, 5);
                            if (byteImg.Length > 0)
                            {
                                imageView.HeightRequest = 150;
                                imageView.BackgroundColor = Color.LightGray;
                            }

                            var s = item.SignaturePath;

                        }
                        else
                        {

                        }
                    }
                    #endregion


                    //#region Add Signature image
                    //if (item.SignatureRequired.GetValueOrDefault())
                    //{
                    //    //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                    //    if (!string.IsNullOrWhiteSpace(item.SignatureString))
                    //    {
                    //        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                    //        byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                    //        // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                    //        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                    //        Case1Grid.Children.Add(imageView, 4, 1);

                    //    }

                    //}
                    //#endregion


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





                    //#region Estimated Hour Region
                    //var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    //var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                    //var estimatedHourLabel = new Label() { TextColor = Color.Black };
                    //estimatedHourLabel.Text = item.EstimatedHours.ToString();
                    //estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                    //estimatedHourStackLayout.Children.Add(estimatedHourLabel);

                    ////mainLayoutGroup.Children.Add(estimatedHourStackLayout);
                    //#endregion


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
                    Title = "Miscellaneous Question ("+ Misce+")",
                    Content = MiscellaneousGrid
                }
            };

            var MiscellGrid = MiscellaneousGrid.Height;
            //layout2Test.HeightRequest = MinimumHeightRequest = 1000;

            var GroupSGrid = MiscellaneousGrid.Height;
            //GroupSecSlCaseTest1.HeightRequest = 2000;

            tabView.Items = tabItems;
            MainLayout.Children.Add(tabView);
        }

        private void GenerateAnswerText(ClosedWorkOrderInspection item)
        {
            this.AnswerText.Append(item.InspectionDescription);
            this.AnswerText.Append(": ");
            this.AnswerText.Append(item.AnswerDescription);
            this.AnswerText.Append(System.Environment.NewLine);
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

            else
            {
                var s = sender as Button;
                s.Text = "";
                this.InspectionCompletionDate = null;
            }

        }
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

            else
            {
                var s = sender as Button;
                s.Text = "";
                this.InspectionStartDate = null;
            }

        }

        protected override async void OnDisappearing()
        {
            total = TimeSpan.Zero;
            TotalInspectionTime = new Label();
            TotalInspectionTime.Text = "";
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
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
}