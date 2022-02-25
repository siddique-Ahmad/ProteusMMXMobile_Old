using Acr.UserDialogs;
using Newtonsoft.Json;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Request;
using ProteusMMX.ViewModel.Miscellaneous;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddInspectionData : ContentPage
    {
        ServiceOutput Inspectiondata;
        private readonly IRequestService _requestService;

        StackLayout MainLayout = new StackLayout();
        public int? WorkorderID { get; set; }
        public AddInspectionData(int? workorderid)
        {
            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
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
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(10);
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
                UserDialogs.Instance.HideLoading();

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




        }

        private async void BindLayout(List<WorkOrderInspectionData> listInspection)
        {
           
            
                StackLayout TabViewSL = new StackLayout();
                TabViewSL.Children.Clear();
                MainLayout.Children.Clear();

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
                #region **** MiscellaneousGrid ****

                Grid MiscellaneousGrid = new Grid { BackgroundColor = Color.White, MinimumWidthRequest = 550, Padding = 0 };
                MiscellaneousGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                StackLayout MiscelMasterSL = new StackLayout();
                MiscellaneousGrid.Children.Add(MiscelMasterSL, 0, 0);
                ScrollView MiscelSV = new ScrollView();
                MiscelMasterSL.Children.Add(MiscelSV);
                StackLayout layout2Test = new StackLayout();
                MiscelSV.Content = layout2Test;
                #endregion

                #region **** GroupSection ***

                Grid GroupSectionsGrid = new Grid { BackgroundColor = Color.White, HeightRequest = 500, Padding = 2 };
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

                foreach (var item in listInspection)
                {
                    StackLayout layout2 = new StackLayout();
                    if (item.SectionName == "Miscellaneous Questions")
                    {

                        foreach (var Miscellaneous in item.sectiondata)
                        {
                            View Question;
                            View Label;
                            View Layout;
                            switch (Miscellaneous.ResponseType)
                            {
                                #region ***** Pass/Fail ****

                                case "Pass/Fail":
                                    StackLayout PassFailSl = new StackLayout();
                                    StackLayout PassFailSlButton = new StackLayout();

                                    var PassFailgrid = new Grid() { HorizontalOptions = LayoutOptions.End, BindingContext = item };
                                    PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                    PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                    PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                                    PassFailSlButton.Children.Add(PassFailgrid);

                                    PassFailSl.Children.Add(PassFailSlButton);

                                    Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, };

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
                                    PassFailgrid.Children.Add(Question, 0, 0);
                                    PassFailgrid.Children.Add(Label, 0, 0);
                                    PassFailgrid.Children.Add(btnTruePF, 1, 0);
                                    PassFailgrid.Children.Add(btnFalsePF, 2, 0);

                                    layout2.Children.Add(PassFailSl);

                                    switch (Miscellaneous.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            break;
                                        case "Fail":
                                            break;
                                    }
                                    break;
                                #endregion

                                #region ***** Standard Range ***
                                case "Standard Range":
                                    StackLayout SRangeSl = new StackLayout();
                                    StackLayout SRangeSlLavel = new StackLayout();
                                    var SRangegrid = new Grid() { BindingContext = item };
                                    SRangegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                    SRangegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 65 });
                                    SRangeSlLavel.Children.Add(SRangegrid);
                                    // StackLayout SRangeSlButton = new StackLayout();

                                    Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                    Layout = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, };
                                    SRangeSl.Children.Add(SRangeSlLavel);
                                    SfBorder RangeBor = new SfBorder() { CornerRadius = 5, WidthRequest = 65, };

                                    (Layout as Entry).BindingContext = new Range() { MaxRange = Miscellaneous.MaxRange, MinRange = Miscellaneous.MinRange };
                                    RangeBor.Content = Layout;
                                    SRangegrid.Children.Add(Question, 0, 0);
                                    SRangegrid.Children.Add(Label, 0, 0);
                                    SRangegrid.Children.Add(RangeBor, 1, 0);

                                    layout2.Children.Add(SRangeSl);

                                    break;
                                #endregion

                                #region ******* Yes/No/N/A ******

                                case "Yes/No/N/A":
                                    StackLayout YesNoSl = new StackLayout();
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
                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black };

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
                                    YesNogrid.Children.Add(Question, 0, 0);
                                    YesNogrid.Children.Add(Label, 0, 0);
                                    Grid.SetColumnSpan(Label, 3);
                                    YesNogrid.Children.Add(btnTrue, 2, 0);
                                    YesNogrid.Children.Add(btnFalse, 3, 0);
                                    YesNogrid.Children.Add(btnNA, 4, 0);

                                    layout2.Children.Add(YesNoSl);
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
                                            //this.btnCreateWorkorder.IsVisible = true;
                                            // BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }
                                    break;
                                #endregion

                                #region ****** Count *****
                                case "Count":
                                    StackLayout CountSl = new StackLayout();
                                    StackLayout CountSlLavel = new StackLayout();
                                    Grid CountGrid = new Grid() { BindingContext = item };

                                    CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                                    CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 65 });
                                    CountSlLavel.Children.Add(CountGrid);
                                    Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };

                                    Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };

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


                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };


                                    Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
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

                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };


                                    MChoiceSlLavel.Children.Add(Label);

                                    Layout = new CustomPicker() { WidthRequest = 65, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Image = "unnamed" };
                                    var index = (Layout as CustomPicker).Items.IndexOf(string.Empty);
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

                                    Label = new Label { Text = Miscellaneous.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                                    Nonegrid.Children.Add(Question, 0, 0);
                                    Nonegrid.Children.Add(Label, 0, 0);
                                    layout2.Children.Add(NoneSl);

                                    break;
                                    #endregion


                            }
                            #region ***** Buttons ***
                            SfButton btnsave = new SfButton
                            {
                                ImageSource = "addinsp.png",
                                FontSize = 16,
                                BorderColor = Color.Black,
                                BorderWidth = 1,
                                CornerRadius = 3,
                                Text = "Add",
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.FromHex("#87CEFA"),
                                TextColor = Color.Black,
                                ImageWidth = 30,
                                HeightRequest = 40,
                                StyleId = Miscellaneous.InspectionID.ToString(),
                            };
                            //var btnsave = new Button() { BackgroundColor = Color.White, ImageSource = "saveicon1.png" };
                            btnsave.Clicked += Btnsave_Clicked;

                            StackLayout Case1SL = new StackLayout();
                            layout2.Children.Add(Case1SL);
                            Grid Case1Grid = new Grid();
                            Case1Grid.Padding = new Thickness(3, 0, 3, 0);
                            Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                            Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            Case1Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            Case1SL.Children.Add(Case1Grid);

                            var estimatedHourTitleLabel = WebControlTitle.GetTargetNameByTitleName("EstimatedHours");
                            var estimatedHourLabel = Miscellaneous.EstimatedHours.ToString();
                            Case1Grid.Children.Add(btnsave, 2, 0);
                            Label Case1lbl = new Label
                            {
                                Text = estimatedHourTitleLabel + ": " + estimatedHourLabel,
                                TextColor = Color.FromHex("#006de0"),
                                VerticalTextAlignment = TextAlignment.Center,
                                VerticalOptions = LayoutOptions.Center,
                                Margin = new Thickness(0, 0, 0, 0)
                            };
                            Case1Grid.Children.Add(Case1lbl, 0, 0);
                            Grid.SetColumnSpan(Case1lbl, 2);
                            BoxView lineBox = new BoxView
                            {
                                HeightRequest = 1,
                                BackgroundColor = Color.Black,
                            };
                            layout2.Children.Add(lineBox);
                            #endregion
                        }
                        layout2Test.Children.Add(layout2);
                    }
                    else
                    {
                        List<WorkOrderInspectionData> commonSections = listInspection.Where(a => a.SectionName != "Miscellaneous Questions").ToList();

                        if (commonSections.Count == 0)
                        {
                            continue;
                        }
                        GroupSecExpCase1 = new SfExpander();
                        GroupSecExpCase1.Header = new Label
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#006de0"),
                            BackgroundColor = Color.WhiteSmoke,
                            HeightRequest = 40,
                            Text = item.SectionName,
                            VerticalTextAlignment = TextAlignment.Center
                        };
                        StackLayout ItemListCase1Sl = new StackLayout();
                        GroupSecExpCase1.Content = ItemListCase1Sl;
                        foreach (var item1 in item.sectiondata)
                        {
                        #region **** Over Load ***
                        //switch (item1.ResponseType)
                        //{
                        //    case "Pass/Fail":
                        //        StackLayout PassFailSl = new StackLayout();
                        //        StackLayout PassFailSlButton = new StackLayout();
                        //        PassFailSl.Children.Add(PassFailSlButton);

                        //        Grid PassFailgrid = new Grid();
                        //        PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        //        PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        //        PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                        //        PassFailgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                        //        PassFailSlButton.Children.Add(PassFailgrid);
                        //        var Questions1 = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                        //        var Label1 = new Label { Text = item1.InspectionDescription, HorizontalTextAlignment = TextAlignment.Start, Font = Font.SystemFontOfSize(14, FontAttributes.None), BindingContext = item1, TextColor = Color.Black, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, };

                        //        var btnTruePF = new SfButton()
                        //        {
                        //            Text = "Pass",
                        //            TextColor = Color.Black,
                        //            FontSize = 11,
                        //            BackgroundColor = Color.LightGray,
                        //            FontAttributes = FontAttributes.Bold,
                        //            CornerRadius = 70,
                        //            HeightRequest = 36
                        //        };
                        //        var btnFalsePF = new SfButton()
                        //        {
                        //            Text = "Fail",
                        //            TextColor = Color.Black,
                        //            FontSize = 11,
                        //            BackgroundColor = Color.LightGray,
                        //            FontAttributes = FontAttributes.Bold,
                        //            CornerRadius = 70,
                        //            HeightRequest = 36
                        //        };


                        //        PassFailgrid.Children.Add(Questions1, 0, 0);
                        //        PassFailgrid.Children.Add(Label1, 0, 0);
                        //        Grid.SetColumnSpan(Label1, 2);
                        //        PassFailgrid.Children.Add(btnTruePF, 2, 0);
                        //        PassFailgrid.Children.Add(btnFalsePF, 3, 0);
                        //        ItemListCase1Sl.Children.Add(PassFailSl);
                        //        switch (item1.AnswerDescription)
                        //        {
                        //            case "":
                        //                break;
                        //            case "Pass":
                        //                break;
                        //            case "Fail":
                        //                break;

                        //        }
                        //        break;

                        //    case "Standard Range":
                        //        StackLayout SRangeSl = new StackLayout();
                        //        StackLayout SRangeSlButton = new StackLayout();
                        //        SRangeSl.Children.Add(SRangeSlButton);
                        //        Grid sta = new Grid();
                        //        sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        //        sta.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        //        sta.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        //        SRangeSlButton.Children.Add(sta);

                        //        var Questions = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                        //        var Label1s = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                        //        SfBorder RangeBor = new SfBorder() { CornerRadius = 5 };

                        //        Entry Layouts = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };

                        //        Layouts.BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                        //        RangeBor.Content = Layouts;
                        //        sta.Children.Add(Questions, 0, 0);
                        //        sta.Children.Add(Label1s, 0, 0);
                        //        sta.Children.Add(RangeBor, 1, 0);
                        //        ItemListCase1Sl.Children.Add(SRangeSl);

                        //        break;

                        //    case "Yes/No/N/A":
                        //        StackLayout YesNoSl = new StackLayout();
                        //        // StackLayout YesNoSlLavel = new StackLayout();
                        //        StackLayout YesNoSlButton = new StackLayout();
                        //        Grid YesNogrid = new Grid();
                        //        YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        //        YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                        //        YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                        //        YesNogrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                        //        YesNoSlButton.Children.Add(YesNogrid);

                        //        // YesNoSl.Children.Add(YesNoSlLavel);
                        //        YesNoSl.Children.Add(YesNoSlButton);
                        //        var Questions1y = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                        //        var Label1y = new Label { Text = item1.InspectionDescription, HorizontalTextAlignment = TextAlignment.Start, Font = Font.SystemFontOfSize(14, FontAttributes.None), BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, TextColor = Color.Black };
                        //        // YesNoSlLavel.Children.Add(Label1);

                        //        var btnTrue = new SfButton()
                        //        {
                        //            Text = "Yes",
                        //            TextColor = Color.Black,
                        //            FontSize = 11,
                        //            BackgroundColor = Color.LightGray,
                        //            FontAttributes = FontAttributes.Bold,
                        //            CornerRadius = 70,
                        //            HeightRequest = 36
                        //        };
                        //        var btnFalse = new SfButton()
                        //        {
                        //            Text = "No",
                        //            TextColor = Color.Black,
                        //            FontSize = 11,
                        //            BackgroundColor = Color.LightGray,
                        //            FontAttributes = FontAttributes.Bold,
                        //            CornerRadius = 70,
                        //            HeightRequest = 36
                        //        };
                        //        var btnNA = new SfButton()
                        //        {
                        //            Text = "NA",
                        //            TextColor = Color.Black,
                        //            FontSize = 11,
                        //            BackgroundColor = Color.LightGray,
                        //            FontAttributes = FontAttributes.Bold,
                        //            CornerRadius = 70,
                        //            HeightRequest = 36
                        //        };



                        //        YesNogrid.Children.Add(Questions1y, 0, 0);
                        //        YesNogrid.Children.Add(Label1y, 0, 0);
                        //        //Grid.SetColumnSpan(Label1y, 2);
                        //        YesNogrid.Children.Add(btnTrue, 1, 0);
                        //        YesNogrid.Children.Add(btnFalse, 2, 0);
                        //        YesNogrid.Children.Add(btnNA, 3, 0);
                        //        //TODO: Add the NA button in grid

                        //        switch (item1.AnswerDescription)
                        //        {
                        //            case "":
                        //                break;
                        //            case "NA":
                        //                break;
                        //            case "Yes":
                        //                break;
                        //            case "No":
                        //                //this.btnCreateWorkorder.IsVisible = true;
                        //                break;

                        //        }


                        //        ItemListCase1Sl.Children.Add(YesNoSl);

                        //        break;

                        //    case "Count":
                        //        StackLayout CountSl = new StackLayout();
                        //        StackLayout CountSlLavel = new StackLayout();
                        //        CountSl.Children.Add(CountSlLavel);
                        //        Grid CountGrid = new Grid();

                        //        CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        //        CountGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        //        CountSlLavel.Children.Add(CountGrid);

                        //        var Questionc = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                        //        var Label1c = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };


                        //        var Layoutc = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };
                        //        (Layoutc as MyEntry).Text = "";

                        //        CountGrid.Children.Add(Questionc, 0, 0);
                        //        CountGrid.Children.Add(Label1c, 0, 0);
                        //        CountGrid.Children.Add(Layoutc, 1, 0);

                        //        ItemListCase1Sl.Children.Add(CountSl);

                        //        break;
                        //    case "Text":
                        //        StackLayout TextSl = new StackLayout();
                        //        StackLayout TextSlLavel = new StackLayout();
                        //        TextSl.Children.Add(TextSlLavel);

                        //        var Textgrid = new Grid() { BindingContext = item };
                        //        Textgrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        //        Textgrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        //        TextSlLavel.Children.Add(Textgrid);

                        //        var Questiont = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                        //        var Label1t = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap };

                        //        var Layoutt = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                        //        (Layoutt as CustomEditor).Text = "";

                        //        SfBorder TextSfBorder = new SfBorder
                        //        {
                        //            BorderColor = Color.Black,
                        //            BorderWidth = 1,
                        //            CornerRadius = 5
                        //        };
                        //        TextSfBorder.Content = Layoutt;
                        //        Textgrid.Children.Add(Questiont, 0, 0);
                        //        Textgrid.Children.Add(Label1t, 0, 0);
                        //        Textgrid.Children.Add(TextSfBorder, 0, 1);

                        //        ItemListCase1Sl.Children.Add(TextSl);

                        //        break;

                        //    case "Multiple Choice":

                        //        StackLayout MChoiceSl = new StackLayout();
                        //        StackLayout MChoiceSlLavel = new StackLayout();
                        //        MChoiceSl.Children.Add(MChoiceSlLavel);
                        //        Grid MChoiceGrid = new Grid();
                        //        MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        //        MChoiceGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        //        MChoiceSlLavel.Children.Add(MChoiceGrid);

                        //        var Questionm = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                        //        var Label1m = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, BindingContext = item1, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                        //        var Layoutm = new CustomPicker() { WidthRequest = 60, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Image = "unnamed" };

                        //        var index = (Layoutm as CustomPicker).Items.IndexOf(item1.AnswerDescription);
                        //        (Layoutm as CustomPicker).SelectedIndex = index;


                        //        MChoiceGrid.Children.Add(Questionm, 0, 0);
                        //        MChoiceGrid.Children.Add(Label1m, 0, 0);
                        //        MChoiceGrid.Children.Add(Layoutm, 1, 0);
                        //        ItemListCase1Sl.Children.Add(MChoiceSl);

                        //        break;

                        //    case "None":
                        //        StackLayout NoneSl = new StackLayout();
                        //        StackLayout NoneSlLavel = new StackLayout();
                        //        NoneSl.Children.Add(NoneSlLavel);
                        //        var Nonegrid = new Grid();
                        //        Nonegrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        //        NoneSlLavel.Children.Add(Nonegrid);

                        //        var Questionn = new Label { Text = "", Font = Font.SystemFontOfSize(14, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                        //        var Label1n = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(14, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, BindingContext = item1, };

                        //        Nonegrid.Children.Add(Questionn, 0, 0);
                        //        Nonegrid.Children.Add(Label1n, 0, 0);
                        //        ItemListCase1Sl.Children.Add(NoneSl);

                        //        break;
                        //}
                        GroupSecSlCase1.Children.Add(GroupSecExpCase1); 
                        #endregion
                    }
                        SfButton btnsave = new SfButton
                        {
                            FontSize = 16,
                            BorderColor = Color.Black,
                            BorderWidth = 1,
                            CornerRadius = 3,
                            Text = "Add",
                            FontAttributes = FontAttributes.Bold,
                            ShowIcon = true,
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            TextColor = Color.Black,
                            ImageSource = "addinsp.png",
                            ImageWidth = 30,
                            HeightRequest = 40,
                        };
                        //var btnsave = new Button() { BackgroundColor = Color.White, HeightRequest = 40, VerticalOptions = LayoutOptions.FillAndExpand, ImageSource = "addbtn.png" };
                        btnsave.Clicked += BtnSaveSection_Clicked;

                        StackLayout Case1SL = new StackLayout();
                        layout2.Children.Add(Case1SL);
                        Grid Case1Grid = new Grid();
                        Case1Grid.Padding = new Thickness(3, 0, 3, 0);
                        Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        Case1Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        Case1Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        Case1SL.Children.Add(Case1Grid);

                        var estimatedHourTitleLabel = WebControlTitle.GetTargetNameByTitleName("EstimatedHours");
                        var estimatedHourLabel = item.EstimatedHours.ToString();

                        Label Case1lbl = new Label
                        {
                            Text = estimatedHourTitleLabel + ": " + estimatedHourLabel,
                            TextColor = Color.FromHex("#006de0"),
                            Margin = new Thickness(0, 0, 0, 0),
                            VerticalTextAlignment = TextAlignment.Center,
                            VerticalOptions = LayoutOptions.Center,
                        };

                        Case1Grid.Children.Add(btnsave, 2, 0);
                        Case1Grid.Children.Add(Case1lbl, 0, 0);
                        Grid.SetColumnSpan(Case1lbl, 2);
                        ItemListCase1Sl.Children.Add(Case1Grid);
                    }
                }

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
                    Title = "Miscellaneous Question ("+singlecount+")",
                    Content = MiscellaneousGrid
                },
                new SfTabItem()
                {
                    Title = "Group Sections ("+Groupcount+")",
                    Content = GroupSectionsGrid
                }
            };

                tabView.Items = tabItems;
                MainLayout.Children.Add(TabViewSL);

                this.Content = MainLayout;
                UserDialogs.Instance.HideLoading();
            

        }

        private async void BtnSaveSection_Clicked(object sender, EventArgs e)
        {
            try
            {

                int FinalSectionID = 0;

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);
                var data = (sender as SfButton).Parent;
                var stacklayout = (sender as SfButton).Parent.Parent.Parent as SfExpander;


                List<InspectionAnswer> listAnswer = new List<InspectionAnswer>();

                var stacklayout1 = stacklayout.Header as Label;
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
                Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/AssociateInspectionsToWorkOrder");

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
                var data = (sender as SfButton).Parent;
                var ssdv = (sender as SfButton).Parent as Grid;




                var kfm = ssdv.Children[0] as SfButton;

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