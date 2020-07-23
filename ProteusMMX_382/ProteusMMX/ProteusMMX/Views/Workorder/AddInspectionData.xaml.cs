using Acr.UserDialogs;
using Newtonsoft.Json;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Request;
using ProteusMMX.ViewModel.Miscellaneous;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddInspectionData : ContentPage
    {
        ServiceOutput Inspectiondata;
        private readonly IRequestService _requestService;
        public int? WorkorderID { get; set; }
        public AddInspectionData(int? workorderid)
        {
            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            this.Title = WebControlTitle.GetTargetNameByTitleName("Inspection");

            WorkorderID = workorderid;

        }

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



                await RetriveAllWorkorderInspectionsAsync();

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message + "<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>" + ex.StackTrace);
            }

        }







        private async Task RetriveAllWorkorderInspectionsAsync()
        {
            try
            {
                string uri = AppSettings.BaseURL + "/Inspection/service/GetWorkorderInspectionData/" + WorkorderID;

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(uri);
                    //HTTP GET
                    var responseTask = client.GetAsync(uri);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                        string readTask = await result.Content.ReadAsStringAsync();
                        Inspectiondata = JsonConvert.DeserializeObject<ServiceOutput>(readTask, _serializerSettings);
                        //readTask.Wait();


                    }
                }
                BindLayout(Inspectiondata.WorkOrderInspectionDataWrapper);

            }
            catch (Exception ex)
            {

                throw;
            }

            // Inspectiondata = await _requestService.GetAsync(uri);            
            //if (CC.listInspection == null || CC.listInspection.Count == 0)
            //{
            //    //this.InspectionTimerLayout.IsVisible = false;
            //    //DisabledText.Text = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
            //    //DisabledText.IsVisible = true;
            //    //return;
            //}




        }




        private void BindLayout(List<WorkOrderInspectionData> listInspection)
        {
            // List<ExistingInspections> listInspection
           // List<WorkOrderInspectionData> distinctList = listInspection.OrderByDescending(x => x.SectionName="Miscellaneous Questions").ToList();
            foreach (var item in listInspection)
            {
                if (item.SectionName == "Miscellaneous Questions")
                {
                    foreach (var Miscellaneous in item.sectiondata)
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
                        switch (Miscellaneous.ResponseType)
                        {
                            case "Pass/Fail":


                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }

                                //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                // GenerateAnswerText(item);

                                var gridPF = new Grid() { HorizontalOptions = LayoutOptions.End };
                                gridPF.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTruePF = new Button() {CornerRadius=5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                    // btnTruePF.Clicked += BtnTrue_Clicked;
                                    // btnFalsePF.Clicked += BtnFalse_Clicked;

                                    gridPF.Children.Add(btnTruePF, 0, 0);
                                    gridPF.Children.Add(btnFalsePF, 1, 0);
                                    Layout = gridPF;
                                    switch (Miscellaneous.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            // BtnTrue_Clicked(btnTruePF, null);
                                            break;
                                        case "Fail":
                                            //this.btnCreateWorkorder.IsVisible = true;
                                            //  BtnFalse_Clicked(btnFalsePF, null);
                                            break;

                                    }
                                }
                                else
                                {
                                    var btnTruePF = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                    //  btnTruePF.Clicked += BtnTrue_Clicked;
                                    // btnFalsePF.Clicked += BtnFalse_Clicked;

                                    gridPF.Children.Add(btnTruePF, 0, 0);
                                    gridPF.Children.Add(btnFalsePF, 1, 0);
                                    Layout = gridPF;
                                    switch (Miscellaneous.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            // BtnTrue_Clicked(btnTruePF, null);
                                            break;
                                        case "Fail":
                                            //this.btnCreateWorkorder.IsVisible = true;

                                            // BtnFalse_Clicked(btnFalsePF, null);
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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }

                                Layout = new MyEntry {  Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start, };

                                // GenerateAnswerText(item);

                                (Layout as MyEntry).BindingContext = new Range() { MaxRange = Miscellaneous.MaxRange, MinRange = Miscellaneous.MinRange };
                                //(Layout as Entry).TextChanged += StandardRange_TextChanged;
                                (Layout as MyEntry).Text = string.Empty;

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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }


                                //  GenerateAnswerText(item);

                                var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTrue = new Button() {CornerRadius=5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                    //Bind the ResponseSubType in Buttons
                                    // btnTrue.BindingContext = Miscellaneous.ResponseSubType;
                                    //  btnFalse.BindingContext = Miscellaneous.ResponseSubType;
                                    //  btnNA.BindingContext = Miscellaneous.ResponseSubType;


                                    //TODO: Add the NA button over here
                                    // btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                    // btnFalse.Clicked += BtnNo_Clicked;
                                    // btnNA.Clicked += BtnNA_Clicked;

                                    grid.Children.Add(btnTrue, 0, 0);
                                    grid.Children.Add(btnFalse, 1, 0);
                                    grid.Children.Add(btnNA, 2, 0);
                                    //TODO: Add the NA button in grid



                                    //TODO: Set the retriving answer accordingly
                                    switch (Miscellaneous.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            //BtnNA_Clicked(btnNA, null);
                                            break;
                                        case "Yes":
                                            // BtnYes_Clicked(btnTrue, null);
                                            break;
                                        case "No":
                                            // BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }

                                    //#region Negative response create workorder btnshow
                                    //if (Miscellaneous.ResponseSubType == null || Miscellaneous.ResponseSubType == false)
                                    //{
                                    //    switch (Miscellaneous.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            // btnCreateWorkorder.IsVisible = true;
                                    //            break;
                                    //        case "No":
                                    //            break;

                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    switch (item.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            break;
                                    //        case "No":
                                    //            //btnCreateWorkorder.IsVisible = true;
                                    //            break;

                                    //    }
                                    //}
                                    //#endregion

                                }
                                else
                                {
                                    var btnTrue = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                    //Bind the ResponseSubType in Buttons
                                    // btnTrue.BindingContext = item.ResponseSubType;
                                    // btnFalse.BindingContext = item.ResponseSubType;
                                    // btnNA.BindingContext = item.ResponseSubType;


                                    //TODO: Add the NA button over here
                                    //  btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                    //  btnFalse.Clicked += BtnNo_Clicked;
                                    //  btnNA.Clicked += BtnNA_Clicked;

                                    grid.Children.Add(btnTrue, 0, 0);
                                    grid.Children.Add(btnFalse, 1, 0);
                                    grid.Children.Add(btnNA, 2, 0);
                                    //TODO: Add the NA button in grid



                                    //TODO: Set the retriving answer accordingly
                                    switch (Miscellaneous.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            // BtnNA_Clicked(btnNA, null);
                                            break;
                                        case "Yes":
                                            // BtnYes_Clicked(btnTrue, null);
                                            break;
                                        case "No":
                                            // BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }

                                    //#region Negative response create workorder btnshow
                                    //if (item.ResponseSubType == null || item.ResponseSubType == false)
                                    //{
                                    //    switch (item.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            // btnCreateWorkorder.IsVisible = true;
                                    //            break;
                                    //        case "No":
                                    //            break;

                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    switch (item.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            break;
                                    //        case "No":
                                    //            // btnCreateWorkorder.IsVisible = true;
                                    //            break;

                                    //    }
                                    //}
                                    //#endregion
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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                // GenerateAnswerText(item);

                                Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };
                                (Layout as MyEntry).Text = string.Empty;
                                // (Layout as Entry).TextChanged += OnlyNumeric_TextChanged;

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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                //  GenerateAnswerText(item);

                                Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                                (Layout as CustomEditor).Text = string.Empty;

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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                // GenerateAnswerText(item);


                                Layout = new Picker() { WidthRequest = 65, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };

                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option1))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option1);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option2))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option2);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option3))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option3);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option4))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option4);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option5))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option5);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option6))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option6);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option7))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option7);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option8))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option8);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option9))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option9);
                                //if (!string.IsNullOrWhiteSpace(Miscellaneous.Option10))
                                //    (Layout as Picker).Items.Add(Miscellaneous.Option10);

                                var index = (Layout as Picker).Items.IndexOf(string.Empty);
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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                //GenerateAnswerText(item);


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

                        var btnsave = new Button() {CornerRadius=5, StyleId=Miscellaneous.InspectionID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Add"), HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                        btnsave.Clicked += Btnsave_Clicked;



                        //#region Add Signature image
                        //if (item.SignatureRequired.GetValueOrDefault())
                        //{
                        //    //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        //    var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                        //    byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                        //    // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                        //    imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        //    layout2.Children.Add(imageView);

                        //    var addSignatureButton = new Button() { Text = WebControlTitle.GetTargetNameByTitleName("AddSignature"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                        //    addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        //    layout2.Children.Add(addSignatureButton);

                        //    var s = item.SignaturePath;
                        //}
                        //#endregion

                        layout2.Children.Add(btnsave);

                        #region Estimated Hour Region
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                            var estimatedHourTitleLabel = new Label() { FontSize = 12, TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                            var estimatedHourLabel = new Label() { FontSize = 12, TextColor = Color.Black };
                            estimatedHourLabel.Text = Miscellaneous.EstimatedHours.ToString();
                            estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                            estimatedHourStackLayout.Children.Add(estimatedHourLabel);
                            mainLayoutGroupSingle.Children.Add(estimatedHourStackLayout);
                        }
                        else
                        {
                            var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                            var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                            var estimatedHourLabel = new Label() { TextColor = Color.Black };
                            estimatedHourLabel.Text = Miscellaneous.EstimatedHours.ToString();
                            estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                            estimatedHourStackLayout.Children.Add(estimatedHourLabel);
                            mainLayoutGroupSingle.Children.Add(estimatedHourStackLayout);
                        }



                        #endregion
                        var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                        mainLayoutGroupSingle.Children.Add(oneBox);

                        MainLayout.Children.Add(mainLayoutGroupSingle);

                        // this.AnswerText.Append(System.Environment.NewLine);


                    }
                }

                else
                {
                    List<WorkOrderInspectionData> commonSections = listInspection.Where(a => a.SectionName != "Miscellaneous Questions").ToList();
                    //  List<WorkOrderInspectionData> commonSections = new List<WorkOrderInspectionData>();

                    //foreach (var item1 in listInspection)
                    //{
                    //    if (item1.IsAdded == false)
                    //    {
                    //        if (item.SectionID == item1.SectionID)
                    //        {
                    //            commonSections.Add(item1);
                    //            item1.IsAdded = true;
                    //        }
                    //    }
                    //}


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


                    string sectionName = "" + item.SectionName;
                    layout1.Children.Add(new Label() { Text = sectionName, Font = Font.SystemFontOfSize(20, FontAttributes.Bold), HorizontalTextAlignment = TextAlignment.Center });

                    //this.AnswerText.Append("Group Name: ");
                    //this.AnswerText.Append(sectionName);
                    //this.AnswerText.Append(System.Environment.NewLine);



                    foreach (var item1 in item.sectiondata)
                    {
                        View Question;
                        View Label1;
                        View Layout;

                        //StackLayout sta;
                        Grid sta;

                        switch (item1.ResponseType)
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
                                // GenerateAnswerText(item1);

                                var gridPF = new Grid() { HorizontalOptions = LayoutOptions.End };
                                gridPF.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTruePF = new Button() {CornerRadius=5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                    // btnTruePF.Clicked += BtnTrue_Clicked;
                                    // btnFalsePF.Clicked += BtnFalse_Clicked;

                                    gridPF.Children.Add(btnTruePF, 0, 0);
                                    gridPF.Children.Add(btnFalsePF, 1, 0);


                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            //BtnTrue_Clicked(btnTruePF, null);
                                            break;
                                        case "Fail":
                                            //this.btnCreateWorkorder.IsVisible = true;
                                            // BtnFalse_Clicked(btnFalsePF, null);
                                            break;

                                    }
                                }
                                else
                                {
                                    var btnTruePF = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                    //btnTruePF.Clicked += BtnTrue_Clicked;
                                    // btnFalsePF.Clicked += BtnFalse_Clicked;

                                    gridPF.Children.Add(btnTruePF, 0, 0);
                                    gridPF.Children.Add(btnFalsePF, 1, 0);


                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            //BtnTrue_Clicked(btnTruePF, null);
                                            break;
                                        case "Fail":
                                            //this.btnCreateWorkorder.IsVisible = true;
                                            // BtnFalse_Clicked(btnFalsePF, null);
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
                                //GenerateAnswerText(item1);

                                Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };

                                (Layout as MyEntry).BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                                //(Layout as Entry).TextChanged += StandardRange_TextChanged;
                                (Layout as MyEntry).Text = string.Empty;


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
                                // GenerateAnswerText(item1);


                                var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTrue = new Button() {CornerRadius=5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                    //Bind the ResponseSubType in Buttons
                                    //btnTrue.BindingContext = item1.ResponseSubType;
                                    //btnFalse.BindingContext = item1.ResponseSubType;
                                    // btnNA.BindingContext = item1.ResponseSubType;

                                    //TODO: Add the NA button over here
                                    //btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                    //btnFalse.Clicked += BtnNo_Clicked;
                                    //btnNA.Clicked += BtnNA_Clicked;

                                    grid.Children.Add(btnTrue, 0, 0);
                                    grid.Children.Add(btnFalse, 1, 0);
                                    grid.Children.Add(btnNA, 2, 0);
                                    //TODO: Add the NA button in grid



                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            //BtnNA_Clicked(btnNA, null);
                                            break;
                                        case "Yes":
                                            // BtnYes_Clicked(btnTrue, null);
                                            break;
                                        case "No":
                                            //this.btnCreateWorkorder.IsVisible = true;
                                            //BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }


                                    //#region Negative response create workorder btnshow
                                    //if (item1.ResponseSubType == null || item1.ResponseSubType == false)
                                    //{
                                    //    switch (item1.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            //   btnCreateWorkorder.IsVisible = true;
                                    //            break;
                                    //        case "No":
                                    //            break;

                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    switch (item1.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            break;
                                    //        case "No":
                                    //            // btnCreateWorkorder.IsVisible = true;
                                    //            break;

                                    //    }
                                    //}
                                    //#endregion
                                }
                                else
                                {
                                    var btnTrue = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                    //Bind the ResponseSubType in Buttons
                                    // btnTrue.BindingContext = item1.ResponseSubType;
                                    // btnFalse.BindingContext = item1.ResponseSubType;
                                    // btnNA.BindingContext = item1.ResponseSubType;

                                    //TODO: Add the NA button over here
                                    // btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                    // btnFalse.Clicked += BtnNo_Clicked;
                                    // btnNA.Clicked += BtnNA_Clicked;

                                    grid.Children.Add(btnTrue, 0, 0);
                                    grid.Children.Add(btnFalse, 1, 0);
                                    grid.Children.Add(btnNA, 2, 0);
                                    //TODO: Add the NA button in grid



                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            // BtnNA_Clicked(btnNA, null);
                                            break;
                                        case "Yes":
                                            // BtnYes_Clicked(btnTrue, null);
                                            break;
                                        case "No":
                                            //this.btnCreateWorkorder.IsVisible = true;
                                            // BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }


                                    //#region Negative response create workorder btnshow
                                    //if (item1.ResponseSubType == null || item1.ResponseSubType == false)
                                    //{
                                    //    switch (item1.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            //  btnCreateWorkorder.IsVisible = true;
                                    //            break;
                                    //        case "No":
                                    //            break;

                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    switch (item1.AnswerDescription)
                                    //    {
                                    //        case "":
                                    //            break;
                                    //        case "NA":
                                    //            break;
                                    //        case "Yes":
                                    //            break;
                                    //        case "No":
                                    //            //  btnCreateWorkorder.IsVisible = true;
                                    //            break;

                                    //    }
                                    //}
                                    //#endregion
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

                                //  GenerateAnswerText(item1);

                                Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };
                                (Layout as MyEntry).Text = string.Empty;
                                //(Layout as Entry).TextChanged += OnlyNumeric_TextChanged;


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
                                // GenerateAnswerText(item1);

                                Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                                (Layout as CustomEditor).Text = string.Empty;



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
                                // GenerateAnswerText(item1);

                                Layout = new Picker() { WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };

                                //if (!string.IsNullOrWhiteSpace(item1.Option1))
                                //    (Layout as Picker).Items.Add(item1.Option1);
                                //if (!string.IsNullOrWhiteSpace(item1.Option2))
                                //    (Layout as Picker).Items.Add(item1.Option2);
                                //if (!string.IsNullOrWhiteSpace(item1.Option3))
                                //    (Layout as Picker).Items.Add(item1.Option3);
                                //if (!string.IsNullOrWhiteSpace(item1.Option4))
                                //    (Layout as Picker).Items.Add(item1.Option4);
                                //if (!string.IsNullOrWhiteSpace(item1.Option5))
                                //    (Layout as Picker).Items.Add(item1.Option5);
                                //if (!string.IsNullOrWhiteSpace(item1.Option6))
                                //    (Layout as Picker).Items.Add(item1.Option6);
                                //if (!string.IsNullOrWhiteSpace(item1.Option7))
                                //    (Layout as Picker).Items.Add(item1.Option7);
                                //if (!string.IsNullOrWhiteSpace(item1.Option8))
                                //    (Layout as Picker).Items.Add(item1.Option8);
                                //if (!string.IsNullOrWhiteSpace(item1.Option9))
                                //    (Layout as Picker).Items.Add(item1.Option9);
                                //if (!string.IsNullOrWhiteSpace(item1.Option10))
                                //    (Layout as Picker).Items.Add(item1.Option10);


                                var index = (Layout as Picker).Items.IndexOf(string.Empty);
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
                                //  GenerateAnswerText(item1);

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

                    var btnSaveSection = new Button() {CornerRadius=5, Text = WebControlTitle.GetTargetNameByTitleName("Add"), CommandParameter = commonSections, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                    btnSaveSection.Clicked += BtnSaveSection_Clicked;







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

                    //this.AnswerText.Append(System.Environment.NewLine);
                    //this.AnswerText.Append(System.Environment.NewLine);

                }

            }
        }










        private async void BtnSaveSection_Clicked(object sender, EventArgs e)
        {
            try
            {

                int FinalSectionID = 0;

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);

                var stacklayout = (sender as Button).Parent as StackLayout;


                List<InspectionAnswer> listAnswer = new List<InspectionAnswer>();




                var stacklayout1 = stacklayout.Children[0] as Label;
                string SectionNameFinal = stacklayout1.Text;
                List<WorkOrderInspectionData> commonSections = Inspectiondata.WorkOrderInspectionDataWrapper.Where(a => a.SectionName != "Miscellaneous Questions").ToList();
                List<WorkOrderInspectionData> FinalSection = commonSections.Where(a => a.SectionName == SectionNameFinal).ToList();
                foreach (var bac in FinalSection)
                {
                    foreach (var item3 in bac.sectiondata)
                    {
                        FinalSectionID = item3.SectionID;
                    }
                }
                Uri posturi =new Uri(AppSettings.BaseURL + "/Inspection/service/AssociateInspectionsToWorkOrder");

                var payload = new Dictionary<string, string>
            {
              {"InspectionID", "0"},
              {"WorkorderID", WorkorderID.ToString()},
               {"SectionID", FinalSectionID.ToString()}
            };

                string strPayload = JsonConvert.SerializeObject(payload);
                HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => SendURI(posturi, c));

                UserDialogs.Instance.HideLoading();
                MainLayout.Children.Clear();
                await Navigation.PopAsync();






            }
            catch (Exception ex)
            {


            }


        }

        private async void Btnsave_Clicked(object sender, EventArgs e)
        {

            try
            {

               

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);

                var ssdv = (sender as Button).Parent as StackLayout;




                var kfm = ssdv.Children[1] as Button;
               
                //  var kfm1 = ParentLayout.Children[0] as StackLayout;

                if (kfm == null)
                {
                    return;
                }

                string FinalInspectionID = kfm.StyleId;


                Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/AssociateInspectionsToWorkOrder");

                var payload = new Dictionary<string, string>
            {
              {"InspectionID", FinalInspectionID},
              {"WorkorderID", WorkorderID.ToString()},
               {"SectionID","0"}
            };

                string strPayload = JsonConvert.SerializeObject(payload);
                HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => SendURI(posturi, c));

                UserDialogs.Instance.HideLoading();
                MainLayout.Children.Clear();
                await Navigation.PopAsync();






            }
            catch (Exception ex)
            {


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



    //public class Range
    //{
    //    public decimal? MaxRange { get; set; }
    //    public decimal? MinRange { get; set; }
    //}
    //public class InspectionTimer
    //{
    //    public int? WorkorderID { get; set; }
    //    public DateTime? InspectionStartTime { get; set; }
    //    public DateTime? InspectionStopTime { get; set; }
    //    public TimeSpan TotalRunningTime { get; set; }
    //    public bool IsTimerRunning { get; set; }

    //}

