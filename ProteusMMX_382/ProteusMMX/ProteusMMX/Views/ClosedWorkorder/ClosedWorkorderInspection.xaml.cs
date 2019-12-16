﻿using Acr.UserDialogs;
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

        public ClosedWorkorderInspection ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            this.Title = WebControlTitle.GetTargetNameByTitleName("Inspection");
           // TotalInspectionTime.Text = WebControlTitle.GetTargetNameByTitleName("TotalInspectionTime") + "(hh:mm:ss)";
            //InspectionTimer.Text = WebControlTitle.GetTargetNameByTitleName("InspectionTimer") + "(hh:mm:ss)";
          
           
           // this.InspectionStartDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("InspectionStartDate");
           // this.InspectionCompletionDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("InspectionCompletionDate");
        }

        ClosedWorkorderInspectionViewModel ViewModel => this.BindingContext as ClosedWorkorderInspectionViewModel;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }



            await OnAppearingOld();





        }


        protected async Task OnAppearingOld()
        {


            try
            {
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
            ClosedWorkorderInspectionList = await ViewModel._inspectionService.GetClosedWorkOrdersInspection(this.ClosedWorkorderID.ToString(),this.UserId);
            if (ClosedWorkorderInspectionList.clWorkOrderWrapper == null || ClosedWorkorderInspectionList.clWorkOrderWrapper.ClosedWorkOrderInspection.Count == 0)
            {
               // this.InspectionTimerLayout.IsVisible = false;
                return;
            }

            foreach (var item in ClosedWorkorderInspectionList.workorderContractor)
            {

                string FinalContractorName = string.Empty;

                Label taskNumberLabel = new Label { TextColor = Color.Black };

                taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": " + item.ContractorName + "   ";

                //Label estimatedHourLabel = new Label { TextColor = Color.Black, };
                //estimatedHourLabel.Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") + ": " + string.Format(StringFormat.NumericZero(), item.EstimatedHours);

                Button startButton = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                    BackgroundColor = Color.FromHex("#D3D3D3"),
                    IsEnabled=false,
                    //CommandParameter = item,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };

                Button stopButton = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                    BackgroundColor = Color.FromHex("#D3D3D3"),
                    IsEnabled = false,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };

               

                Entry hoursEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), TextColor = Color.Black, Placeholder = "hh" };
                Entry minuteEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), TextColor = Color.Black, Placeholder = "mm", };

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
                    startStopButtonGrid.Children.Add(new DatePicker { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Date = Convert.ToDateTime(item.StartDate), }, 8, 0);
                    startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 9, 0);
                    startStopButtonGrid.Children.Add(new DatePicker { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Date = Convert.ToDateTime(item.CompletionDate), }, 10, 0);
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
            foreach (var item in ClosedWorkorderInspectionList.workOrderEmployee)
            {
                string FinalEmployeeName = string.Empty;
                Label taskNumberLabel = new Label { TextColor = Color.Black };

                taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": " + item.EmployeeName + "   ";

                //Label estimatedHourLabel = new Label { TextColor = Color.Black, };
                //estimatedHourLabel.Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") + ": " + string.Format(StringFormat.NumericZero(), item.EstimatedHours);

                Button startButton = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                    IsEnabled = false,
                    BackgroundColor = Color.FromHex("#D3D3D3"),
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };

                Button stopButton = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                    IsEnabled = false,
                    BackgroundColor = Color.FromHex("#D3D3D3"),
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };

                
                Entry hoursEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), TextColor = Color.Black, Placeholder = "hh" };
                Entry minuteEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), TextColor = Color.Black, Placeholder = "mm", };

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
                    startStopButtonGrid.Children.Add(new DatePicker { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Date = Convert.ToDateTime(item.StartDate), }, 8, 0);
                    startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 9, 0);
                    startStopButtonGrid.Children.Add(new DatePicker { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Date = Convert.ToDateTime(item.CompletionDate), }, 10, 0);
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



           
            var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            var FinalLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children =
                                {
                                   new Label{Text="Inspection Timer " +":"+ "hh:mm:ss" ,TextColor=Color.Black}, oneBox

                                }
            };
            ParentLayout.Children.Add(FinalLayout);

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
            foreach (var item in distinctList)
            {
                if (string.IsNullOrEmpty(item.SectionName))
                {
                    var layout1 = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { }

                    };

                    var mainLayoutGroup = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { layout1 }

                    };

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
                    switch (item.ResponseType)
                    {
                        case "Pass/Fail":


                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);

                            var gridPF = new Grid() { HorizontalOptions = LayoutOptions.End };
                            gridPF.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                            var btnTruePF = new Button() {IsEnabled=false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                            var btnFalsePF = new Button() { IsEnabled = false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
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
                                   
                                    BtnFalse_Clicked(btnFalsePF, null);
                                    break;

                            }

                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 1, 0);
                            //sta.Children.Add(LabelHours, 2, 0);
                            layout1.Children.Add(sta);

                            break;

                        case "Standard Range":
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Layout = new Entry() { IsEnabled = false, Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start, };

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
                            layout1.Children.Add(sta);


                            break;

                        case "Yes/No/N/A":
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);

                            var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                            var btnTrue = new Button() { IsEnabled = false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                            var btnFalse = new Button() { IsEnabled = false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                            var btnNA = new Button() { IsEnabled = false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
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

                            Layout = grid;

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
                                     
                                        break;

                                }
                            }
                            #endregion



                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 1, 0);
                            layout1.Children.Add(sta);

                            break;

                        case "Count":
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);

                            Layout = new Entry() { IsEnabled = false, Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };
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
                            layout1.Children.Add(sta);


                            break;
                        case "Text":

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            GenerateAnswerText(item);

                            Layout = new CustomEditor() { IsEnabled = false, HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                            (Layout as CustomEditor).Text = item.AnswerDescription;

                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 0, 1);
                            //sta.Children.Add(LabelHours, 2, 0);
                            layout1.Children.Add(sta);


                            break;

                        case "Multiple Choice":
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);


                            //  Layout = new Picker() {Title=item.AnswerDescription, IsEnabled = false, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };

                            Label Label1 = new Label { Text = item.AnswerDescription,BackgroundColor= Color.FromHex("#d3d3d3"),WidthRequest =40, IsEnabled = false, HorizontalOptions = LayoutOptions.End };

                         
                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Label1, 1, 0);
                            //sta.Children.Add(LabelHours, 2, 0);
                            layout1.Children.Add(sta);


                            break;

                        case "None":

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(16, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

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
                            layout1.Children.Add(sta);


                            break;
                    }

                    

                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                        // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        layout1.Children.Add(imageView);

                       

                        var s = item.SignaturePath;
                    }
                    #endregion

               

                    #region Estimated Hour Region
                    var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                    var estimatedHourLabel = new Label() { TextColor = Color.Black };
                    estimatedHourLabel.Text = item.EstimatedHours.ToString();
                    estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                    estimatedHourStackLayout.Children.Add(estimatedHourLabel);

                    mainLayoutGroup.Children.Add(estimatedHourStackLayout);
                    #endregion

                    var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                    mainLayoutGroup.Children.Add(oneBox);

                    MainLayout.Children.Add(mainLayoutGroup);

                    this.AnswerText.Append(System.Environment.NewLine);


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

                    var layout1 = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { }

                    };
                    //var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };


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
                        View Label;
                        View Layout;

                        //StackLayout sta;
                        Grid sta;

                        switch (item1.ResponseType)
                        {
                            case "Pass/Fail":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                var gridPF = new Grid() { HorizontalOptions = LayoutOptions.End };
                                gridPF.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                                var btnTruePF = new Button() { IsEnabled = false, HorizontalOptions = LayoutOptions.End, Text = "Pass", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                                var btnFalsePF = new Button() { IsEnabled = false, HorizontalOptions = LayoutOptions.End, Text = "Fail", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                                btnTruePF.Clicked += BtnTrue_Clicked;
                                btnFalsePF.Clicked += BtnFalse_Clicked;

                                gridPF.Children.Add(btnTruePF, 0, 0);
                                gridPF.Children.Add(btnFalsePF, 1, 0);
                                Layout = gridPF;

                                switch (item1.AnswerDescription)
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

                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Standard Range":
                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                Layout = new Entry() { IsEnabled = false, Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.End };

                                (Layout as Entry).BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                                (Layout as Entry).TextChanged += StandardRange_TextChanged;
                                (Layout as Entry).Text = item1.AnswerDescription;


                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Yes/No/N/A":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);


                                var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                                var btnTrue = new Button() { IsEnabled = false, HorizontalOptions = LayoutOptions.End, Text = "Yes", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                                var btnFalse = new Button() { IsEnabled = false, HorizontalOptions = LayoutOptions.End, Text = "No", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
                                var btnNA = new Button() { IsEnabled = false, HorizontalOptions = LayoutOptions.End, Text = "NA", TextColor = Color.Blue, BackgroundColor = Color.FromHex("#d3d3d3") };
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

                                Layout = grid;

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
                                          
                                            break;

                                    }
                                }
                                #endregion

                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Count":
                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                //(Label as Label).Text = "If the person is assigned on the inspection task, they don’t get the work order. The only way to get the work order to show to an individual right now is on the main page having them assigned. The system needs to allow the inspection person assigned to go to the mobile based on the inspection task, just as we do the regular tasks. We also need to be able to filter and re-assign an inspection ";


                                GenerateAnswerText(item1);

                                Layout = new Entry() { IsEnabled = false, Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.End };
                                (Layout as Entry).Text = item1.AnswerDescription;
                                (Layout as Entry).TextChanged += OnlyNumeric_TextChanged;


                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;
                            case "Text":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                Layout = new CustomEditor() { IsEnabled = false, HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                                (Layout as CustomEditor).Text = item1.AnswerDescription;



                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(Layout, 0, 1);
                                sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Multiple Choice":
                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                //(Label as Label).Text = "If the person is assigned on the inspection task, they don’t get the work order. The only way to get the work order to show to an individual right now is on the main page having them assigned. The system needs to allow the inspection person assigned to go to the mobile based on the inspection task, just as we do the regular tasks. We also need to be able to filter and re-assign an inspection ";

                                GenerateAnswerText(item1);

                                //Layout = new Picker() {Title=item.AnswerDescription, IsEnabled = false, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
                                 Label   Label1 = new Label { Text = item1.AnswerDescription, BackgroundColor = Color.FromHex("#d3d3d3"), WidthRequest = 40, IsEnabled = false, HorizontalOptions = LayoutOptions.End };




                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(Label1, 1, 0);
                                sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "None":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(16, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                                Label = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                GenerateAnswerText(item1);

                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label, 0, 0);
                                sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                //sta.Children.Add(Layout, 1, 0);
                                layout1.Children.Add(sta);

                                break;
                        }

                    }

               



                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        if (!string.IsNullOrWhiteSpace(item.SignatureString))
                        {
                            var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                            byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                            // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                            imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                            layout1.Children.Add(imageView);
                        }

                       

                    }
                    #endregion

              

                    #region Estimated Hour Region
                    var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                    var estimatedHourLabel = new Label() { TextColor = Color.Black };
                    estimatedHourLabel.Text = item.EstimatedHours.ToString();
                    estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                    estimatedHourStackLayout.Children.Add(estimatedHourLabel);

                    mainLayoutGroup.Children.Add(estimatedHourStackLayout);
                    #endregion
                   var  oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                    mainLayoutGroup.Children.Add(oneBox);

                    MainLayout.Children.Add(mainLayoutGroup);

                    this.AnswerText.Append(System.Environment.NewLine);
                    this.AnswerText.Append(System.Environment.NewLine);

                }

            }




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
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#a0e8a0");
            btn.TextColor = Color.White;
            btn.FontAttributes = FontAttributes.Bold;

            var grid = (sender as Button).Parent as Grid;
            var btnFalse = grid.Children[1] as Button;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnFalse.BackgroundColor = Color.FromHex("#d3d3d3");

        }

        private void BtnFalse_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#ff8080");

            var grid = (sender as Button).Parent as Grid;
            var btnFalse = grid.Children[0] as Button;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnFalse.BackgroundColor = Color.FromHex("#d3d3d3");

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
                btn.BackgroundColor = Color.FromHex("#a0e8a0");
               
            }
            else
            {
                btn.BackgroundColor = Color.FromHex("#ff8080");

            }

            btnFalse.BackgroundColor = Color.FromHex("#d3d3d3");
            btnNA.BackgroundColor = Color.FromHex("#d3d3d3");

        }

        private void BtnNo_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#ff8080");

            var grid = (sender as Button).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnYes = grid.Children[0] as Button;
            var btnNA = grid.Children[2] as Button;
            //TODO: Get the button color from the btn object set the inverse color over here.
            if (!responseSubType)
            {
                btn.BackgroundColor = Color.FromHex("#a0e8a0");


            }
            else
            {
                btn.BackgroundColor = Color.FromHex("#ff8080");

            }

            btnYes.BackgroundColor = Color.FromHex("#d3d3d3");
            btnNA.BackgroundColor = Color.FromHex("#d3d3d3");

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
            btnTrue.BackgroundColor = Color.FromHex("#d3d3d3");
            btnFalse.BackgroundColor = Color.FromHex("#d3d3d3");

        }

        private void PassFail_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn.Text == "Pass")
            {
                btn.Text = "Fail";
                btn.BackgroundColor = Color.FromHex("#ff8080");
            }

            else
            {
                btn.Text = "Pass";
                btn.BackgroundColor = Color.FromHex("#a0e8a0");
               
            }
        }

        private void YesNo_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn.Text == "Yes")
            {
                btn.Text = "No";
                btn.BackgroundColor = Color.FromHex("#ff8080");

            }

            else
            {
                btn.Text = "Yes";
                btn.BackgroundColor = Color.FromHex("#a0e8a0");

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