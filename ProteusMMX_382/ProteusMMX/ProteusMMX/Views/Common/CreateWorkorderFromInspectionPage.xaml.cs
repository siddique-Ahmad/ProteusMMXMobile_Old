using Acr.UserDialogs;
using Newtonsoft.Json;
using NodaTime;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class CreateWorkorderFromInspectionPage : PopupPage
    {
        
        private int? WorkorderID;
        private string baseURL;
        private string UserID = AppSettings.User.UserID.ToString();
        private StringBuilder answerText;
        private ServiceOutput abc;
        string UserTimeZone = AppSettings.ClientIANATimeZone;

        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        ITaskAndLabourService TaskAndLabourService = Locator.Instance.Resolve<ITaskAndLabourService>();
        public CreateWorkorderFromInspectionPage(int? workorderID, StringBuilder answerText)
        {
            InitializeComponent();
            this.WorkorderID = workorderID;
            this.answerText = answerText;

            this.AdditionalDetails.Text = answerText.ToString();
            this.DescriptionLabel.Text = WebControlTitle.GetTargetNameByTitleName("Description");
            this.AdditionalDetailsLabel.Text = WebControlTitle.GetTargetNameByTitleName("AdditionalDetails");
            CreateWo.Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder");

        }

        private async void Mic_Clicked(object sender, EventArgs e)
        {
            //string Value = await DependencyService.Get<ISpeechToText>().SpeechToText();
            //AdditionalDetails.Text += " " + Value;
        }
        private async void CreateWorkorder_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                abc = await WorkorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());

                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {

                    var workOrder = new workOrders();

                    workOrder.RequiredDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date;
                    workOrder.Description = String.IsNullOrWhiteSpace(Description.Text) ? null : Description.Text.Trim();
                    workOrder.AdditionalDetails = AdditionalDetails.Text;
                    workOrder.LocationID = abc.workOrderWrapper.workOrder.LocationID;
                    workOrder.FacilityID = 0;
                    workOrder.AssetID = abc.workOrderWrapper.workOrder.AssetID;
                    workOrder.AssetSystemID = abc.workOrderWrapper.workOrder.AssetSystemID;
                    workOrder.AssignedToEmployeeID = abc.workOrderWrapper.workOrder.AssignedToEmployeeID;
                    workOrder.IsSignatureValidated = true;
                    Application.Current.Properties["WorkorderWrapperInspection"] = workOrder;
                    await PopupNavigation.PopAsync();
                    var page = new CreateworkorderSignatureFromInspectionPage();
                    await page.Navigation.PushPopupAsync(page);


                    


                }
                else
                {
                   

                    var yourobject = new workOrderWrapper
                    {
                        TimeZone = "UTC",
                        CultureName = "en-US",
                        UserId = Convert.ToInt32(UserID),
                        ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                        workOrder = new workOrders
                        {
                            RequiredDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,
                            Description = String.IsNullOrWhiteSpace(Description.Text) ? null : Description.Text.Trim(),
                            AdditionalDetails = AdditionalDetails.Text,
                            LocationID = abc.workOrderWrapper.workOrder.LocationID,
                            FacilityID = 0,
                            AssetID = abc.workOrderWrapper.workOrder.AssetID,
                            AssetSystemID = abc.workOrderWrapper.workOrder.AssetSystemID,
                            AssignedToEmployeeID = abc.workOrderWrapper.workOrder.AssignedToEmployeeID,
                            IsSignatureValidated=false,

                        },

                    };


                    ServiceOutput res = await WorkorderService.CreateWorkorder(yourobject);
                    if (res != null)
                    {

                        ServiceOutput assignto = await WorkorderService.GetEmployeeAssignTo(UserID);
                        if (assignto.EmployeeLaborCraftID == 0 || string.IsNullOrWhiteSpace(assignto.EmployeeLaborCraftID.ToString()))
                        {
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                            UserDialogs.Instance.HideLoading();
                            return;

                        }

                        var yourobject1 = new workOrderWrapper
                        {
                            TimeZone = "UTC",
                            CultureName = "en-US",
                            ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                            workOrderLabor = new WorkOrderLabor
                            {

                                WorkOrderID = res.workOrderWrapper.workOrder.WorkOrderID,

                                TaskID = null,

                                StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,

                                EmployeeLaborCraftID = assignto.EmployeeLaborCraftID



                            },

                        };


                        var status = await TaskAndLabourService.CreateWorkOrderLabor(yourobject1);



                        if (Boolean.Parse(status.servicestatus))
                        {
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                            UserDialogs.Instance.HideLoading();
                            OnClose(null, null);


                        }



                    }
                    else
                    {
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("SomethingWentWrong"));
                    }
                }
              
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                OnClose(null, null);
                UserDialogs.Instance.HideLoading();
            }
           
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

    }
}