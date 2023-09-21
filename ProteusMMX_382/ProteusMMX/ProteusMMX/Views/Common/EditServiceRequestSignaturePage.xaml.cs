using Acr.UserDialogs;
using ProteusMMX.Controls;
using ProteusMMX.Crypto;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.ServiceRequestModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.ServiceRequest;
using ProteusMMX.Services.Workorder;
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
    public partial class EditServiceRequestSignaturePage : PopupPage
    {
        string password = string.Empty;
        IServiceRequestModuleService ServiceRequestService = Locator.Instance.Resolve<IServiceRequestModuleService>();
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        //IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string CauseID = string.Empty;
        ServiceRequests SRwrapper = null;
        Cause CauseJson;
        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;
        ServiceOutput user;


        public EditServiceRequestSignaturePage()
        {
            InitializeComponent();
            HeaderLabel.Text = WebControlTitle.GetTargetNameByTitleName("Signature") + " " + WebControlTitle.GetTargetNameByTitleName("For") + " " + AppSettings.User.UserName + " " + WebControlTitle.GetTargetNameByTitleName("On") + " " + WebControlTitle.GetTargetNameByTitleName("ServiceRequest");
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //OnDispose?.Invoke();
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

                if (Application.Current.Properties.ContainsKey("ServiceRequestWrapper"))
                {
                    SRwrapper = Application.Current.Properties["ServiceRequestWrapper"] as ServiceRequests;

                }
                var serviceRequest = new ServiceRequestWrapper
                {
                    TimeZone = "UTC",
                    UserId = Convert.ToInt32(AppSettings.User.UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    CultureName = "en-US",
                    serviceRequest = SRwrapper,

                };


                var response = await ServiceRequestService.EditServiceRequest(serviceRequest);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    Application.Current.Properties["UserNameType"] = "Label";
                    SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                    await navigationService.NavigateBackAsync();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Servicerequestsuccessfullyupdated"), 2000);

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