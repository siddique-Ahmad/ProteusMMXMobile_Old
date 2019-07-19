using Acr.UserDialogs;
using NodaTime;
using ProteusMMX.Controls;
using ProteusMMX.Crypto;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Workorder;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SignaturePad.Forms;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class CreateworkorderSignatureFromInspectionPage : PopupPage
    {
        string password = string.Empty;
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        ITaskAndLabourService TaskLabourService = Locator.Instance.Resolve<ITaskAndLabourService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string CauseID = string.Empty;
        workOrders wowrapper = null;
        Cause CauseJson;
        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;
        ServiceOutput user;
        public CreateWorkorderPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as CreateWorkorderPageViewModel;
            }
        }
        public CreateworkorderSignatureFromInspectionPage()
        {
            InitializeComponent();
            HeaderLabel.Text = WebControlTitle.GetTargetNameByTitleName("Signature") + " " + WebControlTitle.GetTargetNameByTitleName("For") + " " + AppSettings.User.UserName + " " + WebControlTitle.GetTargetNameByTitleName("On") + " " + WebControlTitle.GetTargetNameByTitleName("WorkOrders");
            if (Application.Current.Properties.ContainsKey("UserNameType"))
            {
                string UsernameType = Application.Current.Properties["UserNameType"].ToString();
                if (UsernameType == "TextBox")
                {
                    Application.Current.Properties["UserNameType"] = "TextBox";
                    UserLabel.Text = "";
                    UserLabel.IsEnabled = true;
                }
                else
                {
                    Application.Current.Properties["UserNameType"] = "Label";
                    UserLabel.Text = AppSettings.User.UserName;
                    UserLabel.IsEnabled = false;
                }
            }
            Username.Text = WebControlTitle.GetTargetNameByTitleName("UserName");
            Pass.Text = WebControlTitle.GetTargetNameByTitleName("Password");
            ok.Text = WebControlTitle.GetTargetNameByTitleName("OK");

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

        private async void OK_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                if (Application.Current.Properties.ContainsKey("Password"))
                {
                    password = Application.Current.Properties["Password"].ToString();

                }
                if (AppSettings.User.UserName.ToLower() != UserLabel.Text.ToLower() || password != PassLabel.Text)
                {
                    UserDialogs.Instance.HideLoading();
                    await DialogService.ShowAlertAsync(WebControlTitle.GetTargetNameByTitleName("InvalidCredentials"), WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    return;
                }

                if (Application.Current.Properties.ContainsKey("WorkorderWrapperInspection"))
                {
                    wowrapper = Application.Current.Properties["WorkorderWrapperInspection"] as workOrders;

                }
                //var workorder = new workOrderWrapper
                //{
                   
                //    TimeZone = AppSettings.UserTimeZone,
                //    CultureName = AppSettings.UserCultureName,
                //    UserId = Convert.ToInt32(AppSettings.User.UserID),
                //    ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                //    workOrder = wowrapper,
                  


                //};
                var yourobject = new workOrderWrapper
                {
                    TimeZone = "UTC",
                    CultureName = "en-US",
                    UserId = Convert.ToInt32(AppSettings.User.UserID),
                    ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                    workOrder = new workOrders
                    {
                        RequiredDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,
                        Description = String.IsNullOrWhiteSpace(wowrapper.Description) ? null : wowrapper.Description.Trim(),
                        AdditionalDetails = wowrapper.AdditionalDetails,
                        LocationID =wowrapper.LocationID,
                        FacilityID = 0,
                        AssetID = wowrapper.AssetID,
                        AssetSystemID = wowrapper.AssetSystemID,
                        AssignedToEmployeeID = wowrapper.AssignedToEmployeeID,
                        ModifiedUserName=AppSettings.User.UserName,
                        IsSignatureValidated = true,

                    },

                };



                ServiceOutput res = await WorkorderService.CreateWorkorder(yourobject);
                if (res != null)
                {

                    ServiceOutput assignto = await WorkorderService.GetEmployeeAssignTo(AppSettings.User.UserID.ToString());
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


                    var status = await TaskLabourService.CreateWorkOrderLabor(yourobject1);



                    if (Boolean.Parse(status.servicestatus))
                    {
                        Application.Current.Properties["UserNameType"] = "Label";
                        SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
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
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (AppSettings.User.UserName.ToLower() != UserLabel.Text.ToLower() || password != PassLabel.Text)
                {
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    OnClose(null, null);
                    UserDialogs.Instance.HideLoading();
                }
            }

        }


    }

}