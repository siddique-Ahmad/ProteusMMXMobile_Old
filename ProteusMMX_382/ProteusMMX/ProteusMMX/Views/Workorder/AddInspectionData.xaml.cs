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
using System.Collections.ObjectModel;
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
        ListView Groupview = new ListView();
        public ObservableCollection<string> GroupList = new ObservableCollection<string>();
       
        Dictionary<string,int> GroupCompareList = new Dictionary<string, int>();
        ServiceOutput Inspectiondata;
        List<WorkOrderInspectionData> Inspections;
        private readonly IRequestService _requestService;

        StackLayout MainLayout = new StackLayout();
        public int? WorkorderID { get; set; }
        public AddInspectionData(int? workorderid)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
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


            foreach (var item in listInspection)
            {
                StackLayout layoutBoth = new StackLayout();
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

                                layoutBoth.Children.Add(PassFailSl);

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

                                layoutBoth.Children.Add(SRangeSl);

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

                                layoutBoth.Children.Add(YesNoSl);
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
                                layoutBoth.Children.Add(CountSl);

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

                                layoutBoth.Children.Add(TextSl);
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
                                layoutBoth.Children.Add(MChoiceSl);


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
                                layoutBoth.Children.Add(NoneSl);

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
                        layoutBoth.Children.Add(Case1SL);
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
                        layoutBoth.Children.Add(lineBox);
                        #endregion
                    }
                    layout2Test.Children.Add(layoutBoth);
                }
                else
                {
                    GroupList.Add(item.SectionName);
                    GroupCompareList.Add(item.SectionName, item.SectionID);
                }
                Groupview.ItemsSource = GroupList;
                
            }

            Groupview.ItemSelected += Groupview_ItemSelected;
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
                    Content = Groupview
                }
            };
            tabView.Items = tabItems;
            MainLayout.Children.Add(TabViewSL);

            this.Content = MainLayout;
            UserDialogs.Instance.HideLoading();
        }
   
        private async void Groupview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);
                int FinalSectionID = 0;
                string selectedSection = (string)e.SelectedItem;
                var SelectedSectionID = GroupCompareList.FirstOrDefault(x => x.Key == selectedSection).Value;
                FinalSectionID = SelectedSectionID;
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

