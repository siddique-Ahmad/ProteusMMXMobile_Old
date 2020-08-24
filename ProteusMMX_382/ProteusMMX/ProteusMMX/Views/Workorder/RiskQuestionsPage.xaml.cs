using Acr.UserDialogs;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder.Inspection;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RiskQuestionsPage : ContentPage
    {
        string SwitchValue;
        Button btnsave;
        StackLayout layout1;
        StackLayout mainLayoutGroup;
        List<string> ResponsetypeName = new List<string>();
        List<RiskInspectionAnswers> listAnswers = new List<RiskInspectionAnswers>();
        List<RiskInspectionAnswers> listquestions = new List<RiskInspectionAnswers>();
        List<RiskInspectionAnswers> listfinalquestions = new List<RiskInspectionAnswers>();
        List<RiskInspectionAnswers> FinalQuestionList = new List<RiskInspectionAnswers>();
        List<string> listquestions1 = new List<string>();


        ServiceOutput ListRiskQuestion;
        protected readonly INavigationService NavigationService;
        Label SectionLabel;
        ProgressBar pBar = new ProgressBar();
        public readonly IInspectionService _inspectionService;
        public int? WorkorderID { get; set; }
        RiskQuestionPageViewModel ViewModel => this.BindingContext as RiskQuestionPageViewModel;
        public RiskQuestionsPage()
        {
            InitializeComponent();

            this.Title = WebControlTitle.GetTargetNameByTitleName("RiskQuestion");
            NavigationService = Locator.Instance.Resolve<INavigationService>();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;



        }
       
        public bool _operationInProgress;
        public bool OperationInProgress
        {
            get
            {
                return _operationInProgress;
            }

            set
            {

                if (value != _operationInProgress)
                {
                    _operationInProgress = value;
                    OnPropertyChanged("OperationInProgress");

                }
            }
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
                if (Application.Current.Properties.ContainsKey("WorkorderID"))
                {
                    var workorderid = Application.Current.Properties["WorkorderID"].ToString();
                    if (workorderid != null)
                    {
                        this.WorkorderID = Convert.ToInt32(workorderid);

                    }
                }
              //  this.WorkorderID = ViewModel.WorkorderID;
                MainLayout.Children.Clear();
                await RetriveAllRiskQuestionsAsync();

               

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message + "<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>" + ex.StackTrace);
            }

        }







        private async Task RetriveAllRiskQuestionsAsync()
        {

            ListRiskQuestion = await ViewModel._inspectionService.GetRiskQuestions(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
            BindLayout(ListRiskQuestion.workOrderWrapper.RiskQuestions);

        }
        private void BindLayout(List<RiskInspectionAnswers> listRiskQuestions)
        {
            foreach (var item in listRiskQuestions)
            {
                listquestions1.Add(item.QuestionDescription);
                SectionLabel = new Label { Text = item.SectionName, FontSize = 25, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold };
            }
           

            foreach(var item in listRiskQuestions)
            {
               
                Label lblnew= new Label { Text = item.QuestionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                CustomSwitch switcher = new CustomSwitch
                {
                    StyleId=item.QuestionDescription,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                switcher.Toggled += switcher_Toggled;
                mainLayoutGroup = new StackLayout
                {
                    Spacing=20,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = { lblnew,switcher }

                };
                MainLayout.Children.Add(SectionLabel);
                MainLayout.Children.Add(mainLayoutGroup);
            }
            btnsave = new Button() { Text = WebControlTitle.GetTargetNameByTitleName("Save")+"*", HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("#87CEFA"),CornerRadius=5, TextColor = Color.White, BorderColor = Color.Black };
            btnsave.Clicked += Btnsave_Clicked;
            Label lbl = new Label { HorizontalTextAlignment = TextAlignment.Center, Text = WebControlTitle.GetTargetNameByTitleName("SavewillbeconsideredasESignature") };
            MainLayout.Children.Add(btnsave);
            MainLayout.Children.Add(lbl);

        }
        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            
           CustomSwitch thisSender = (CustomSwitch)sender;
            foreach (var item in listquestions1)
            {
                if (item == thisSender.StyleId)
                {
                    listquestions.Add(new RiskInspectionAnswers()
                    {
                        QuestionDescription=item,
                        AnswerDescription = string.IsNullOrWhiteSpace(e.Value.ToString()) ? "" : e.Value.ToString(),
                        WorkOrderID = WorkorderID

                    });
                }
            }
            
        }
    
        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            try
            {
                var distinctList = listquestions.GroupBy(x => x.QuestionDescription)
                           .Select(g => g.Last())
                           .ToList();

                listfinalquestions = distinctList;
                //if (listfinalquestions.Count == 0)
                //{
                //    await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Pleasefillallanswer"), "OK");
                //    return;
                //}

                var List2 = ListRiskQuestion.workOrderWrapper.RiskQuestions.Where(item => !listfinalquestions.Any(item2 => item2.QuestionDescription == item.QuestionDescription));
                //FinalQuestionList = List2;
                listfinalquestions.AddRange(List2);
                var things = (from at in listfinalquestions
                              join st in ListRiskQuestion.workOrderWrapper.RiskQuestions on at.QuestionDescription equals st.QuestionDescription into items
                              from item in items.DefaultIfEmpty()
                              orderby item.InspectionID
                              select at).ToList();

                FinalQuestionList = things;
              
                btnsave.IsEnabled = false;
                btnsave.BackgroundColor = Color.Gray;
               
               
                for (int i = 0; i < ListRiskQuestion.workOrderWrapper.RiskQuestions.Count(); i++)
                {
                    var item = ListRiskQuestion.workOrderWrapper.RiskQuestions[i];
                    try
                    {
                        SwitchValue = FinalQuestionList[i].AnswerDescription.ToString();
                        if (SwitchValue == "True")
                        {
                            SwitchValue = "Yes";
                        }
                        else
                        {
                            SwitchValue = "No";
                        }
                    }
                    catch (Exception)
                    {

                        SwitchValue = "No";
                    }
                 
                   
                    listAnswers.Add(new RiskInspectionAnswers()
                    {
                        InspectionID = item.InspectionID,
                        QuestionDescription = item.QuestionDescription,
                        AnswerDescription = SwitchValue,
                        WorkOrderID = item.WorkOrderID,
                        ModifiedUserName=AppSettings.User.UserName,
                    });
                }

                
                #region Save Answer to server
                var yourobject = new workOrderWrapper
                {

                    TimeZone = AppSettings.UserTimeZone,
                  
                    CultureName = AppSettings.UserCultureName,
                    UserId = AppSettings.User.UserID,
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    RiskQuestions = listAnswers,
                





                };


                var response = await ViewModel._inspectionService.RiskAnswers(yourobject);
                if (response != null && bool.Parse(response.servicestatus) && response.servicestatusmessge == "success")
                {
                    listAnswers.Clear();
                    if (Application.Current.Properties.ContainsKey("Workorderitem"))
                    {
                        workOrders listitems = Application.Current.Properties["Workorderitem"] as workOrders;
                        await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(listitems);
                        await NavigationService.RemoveLastFromBackStackAsync();


                    }


                }
                else
                {

                    listAnswers.Clear();
                    await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Pleasefillallpositiveanswer"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    await NavigationService.NavigateBackAsync();



                }




            }
            catch (Exception ex)
            {

            }
            finally
            {

            }




            #endregion



        }
      

    }
}


