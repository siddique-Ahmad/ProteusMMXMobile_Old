using Acr.UserDialogs;
using NodaTime;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Views.Common;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class CreateWorkorderFromInspectionPageContent : ContentPage
    {
        public string CurrentRuntimeValue;
        private int? WorkorderID;
        private string baseURL;
        private string UserID = AppSettings.User.UserID.ToString();
        private StringBuilder answerText;
        private ServiceOutput abc;
        string UserTimeZone = AppSettings.ClientIANATimeZone;

        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        ITaskAndLabourService TaskAndLabourService = Locator.Instance.Resolve<ITaskAndLabourService>();
        public CreateWorkorderFromInspectionPageContent(int? workorderID, StringBuilder answerText)
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
                if (abc.workOrderWrapper.workOrder.CurrentRuntime != null)
                {
                    CurrentRuntimeValue = abc.workOrderWrapper.workOrder.CurrentRuntime;
                }
                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {
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
                        await Navigation.PopAsync();
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
                                CurrentRuntime = CurrentRuntimeValue,
                                AssetID = abc.workOrderWrapper.workOrder.AssetID,
                                AssetSystemID = abc.workOrderWrapper.workOrder.AssetSystemID,
                                AssignedToEmployeeID = abc.workOrderWrapper.workOrder.AssignedToEmployeeID,
                                IsSignatureValidated = false,
                                ModifiedUserName= AppSettings.UserName+  "|"  +"InspectionWorkorderFromMobile",


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
                                await Navigation.PopAsync();
                                //  OnClose(null, null);


                            }



                        }
                        else
                        {
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("SomethingWentWrong"));
                        }
                    }
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
                            CurrentRuntime = CurrentRuntimeValue,
                            AssetID = abc.workOrderWrapper.workOrder.AssetID,
                            AssetSystemID = abc.workOrderWrapper.workOrder.AssetSystemID,
                            AssignedToEmployeeID = abc.workOrderWrapper.workOrder.AssignedToEmployeeID,
                            IsSignatureValidated = false,
                            ModifiedUserName = AppSettings.UserName +  "|"  + "InspectionWorkorderFromMobile",

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
                            await Navigation.PopAsync();
                            //  OnClose(null, null);


                        }



                    }
                    else
                    {
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("SomethingWentWrong"));
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                //await Navigation.PopAsync();
                UserDialogs.Instance.HideLoading();
            }

        }

       

    }
}