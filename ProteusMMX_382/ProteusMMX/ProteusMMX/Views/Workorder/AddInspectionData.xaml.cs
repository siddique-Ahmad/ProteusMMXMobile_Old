using Acr.UserDialogs;
using Newtonsoft.Json;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Request;
using ProteusMMX.ViewModel.Miscellaneous;
using Syncfusion.XForms.Expander;
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

        StackLayout MainLayout = new StackLayout();
        StackLayout MainLayoutGroup = new StackLayout();
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
            UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
            await Task.Delay(1000);
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
                UserDialogs.Instance.HideLoading();
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

            UserDialogs.Instance.HideLoading();


        }




        private async void  BindLayout(List<WorkOrderInspectionData> listInspection)
        {
            int singlecount = 0;
            int Groupcount;
            try
            {

                var filteredResult = from s in listInspection
                                     where s.SectionName == "Miscellaneous Questions"
                                     select s;
                if (filteredResult.Any())
                {
                    singlecount = filteredResult.ElementAt(0).sectiondata.Count;
                }
                else
                {
                    singlecount = 0;
                }

                Groupcount = listInspection.Where(a => a.SectionName != "Miscellaneous Questions").Count();
            }
            catch (Exception ex)
            {

                throw;
            }


            //Expander One
            SfExpander expander1;
            expander1 = new SfExpander();

            SfExpander expander;
            ScrollView scrollView;
            StackLayout stack;
            scrollView = new ScrollView();
            scrollView.Margin = 20;

            stack = new StackLayout();

            //Expander two
            expander = new SfExpander();

            //Expander Header for Single
            var headergrid = new Grid()
            {
                HeightRequest = 60
            };
            var headerLabel = new Label()
            {
                TextColor = Color.Black,
                FontSize=15,
                FontAttributes=FontAttributes.Bold,
                BackgroundColor = Color.LightSkyBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Miscellaneous Questions" + "(" + singlecount + ")",
                VerticalTextAlignment = TextAlignment.Center
            };
            headergrid.Children.Add(headerLabel);
            expander.Header = headergrid;


            //Expander Header for Group
            var headergrid1 = new Grid()
            {
                HeightRequest = 60
            };
            var headerLabel1 = new Label()
            {
                TextColor = Color.Black,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.LightSkyBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Group Sections" + "(" + Groupcount + ")",
                VerticalTextAlignment = TextAlignment.Center
            };
            headergrid1.Children.Add(headerLabel1);
            expander1.Header = headergrid1;


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

                        var mainLayoutGroupSingle = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Children = { layout2 }

                        };




                        View Question;
                        View Label;
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
                                    var btnTruePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
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

                                Layout = new MyEntry { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start, };

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

                                var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTrue = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };

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


                                }
                                else
                                {
                                    var btnTrue = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };

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


                                Layout = new MyPicker() { WidthRequest = 65, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };


                                var index = (Layout as MyPicker).Items.IndexOf(string.Empty);
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
                        var btnSave = new Button() { StyleId = Miscellaneous.InspectionID.ToString(), CornerRadius = 5, Text = WebControlTitle.GetTargetNameByTitleName("Add"), WidthRequest = 300, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                        btnSave.Clicked += Btnsave_Clicked;
                        layout2.Children.Add(btnSave);
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
                    }

                }
                else
                {
                    List<WorkOrderInspectionData> commonSections = listInspection.Where(a => a.SectionName != "Miscellaneous Questions").ToList();

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

                    var mainLayoutGroup = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { layout1 }

                    };

                    string sectionName = "" + item.SectionName;
                    layout1.Children.Add(new Label() { Text = sectionName, Font = Font.SystemFontOfSize(20, FontAttributes.Bold), HorizontalTextAlignment = TextAlignment.Center });

                    //foreach (var item1 in item.sectiondata)
                    //{
                    //    View Question;
                    //    View Label1;
                    //    View Layout;

                    //    //StackLayout sta;
                    //    Grid sta;

                    //    Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };


                    //    if (Device.Idiom == TargetIdiom.Phone)
                    //    {
                    //        Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                    //    }
                    //    else
                    //    {
                    //        Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                    //    }
                    //    sta = new Grid() { };
                    //    sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    //    sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                    //    sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    //    sta.Children.Add(Question, 0, 0);
                    //    sta.Children.Add(Label1, 0, 0);
                    //    layout1.Children.Add(sta);
                    //}

                    var btnSaveSection = new Button() { CornerRadius = 5, Text = WebControlTitle.GetTargetNameByTitleName("Add"), WidthRequest = 300, HorizontalOptions = LayoutOptions.Center, CommandParameter = commonSections, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
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
                    MainLayoutGroup.Children.Add(mainLayoutGroup);

                }

            }
            expander.Content = MainLayout;
            expander1.Content = MainLayoutGroup;
            stack.Children.Add(expander);
            stack.Children.Add(expander1);
            scrollView.Content = stack;
            this.Content = scrollView;

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

