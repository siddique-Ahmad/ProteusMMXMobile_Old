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
using ProteusMMX.ViewModel.ServiceRequest;
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
    public partial class AcceptServiceRequestSignaturePage : PopupPage
    {
        string password = string.Empty;
        IServiceRequestModuleService ServiceRequestService = Locator.Instance.Resolve<IServiceRequestModuleService>();
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string CauseID = string.Empty;
        workOrders wowrapper = null;
        Cause CauseJson;
        string SRID = string.Empty;
        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;
        string SRNavigation = string.Empty;
        ServiceOutput user;

        public AcceptServiceRequestSignaturePage()
        {
            InitializeComponent();
            HeaderLabel.Text = WebControlTitle.GetTargetNameByTitleName("Signature")+" "+ WebControlTitle.GetTargetNameByTitleName("For")+" " + AppSettings.User.UserName +" " + WebControlTitle.GetTargetNameByTitleName("On")+" "+ WebControlTitle.GetTargetNameByTitleName("ServiceRequest");
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
            ok.Text= WebControlTitle.GetTargetNameByTitleName("OK");

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
                var username = AppSettings.User.UserName;
                if (AppSettings.User.UserName.ToLower() != UserLabel.Text.ToLower() || password != PassLabel.Text)
                {
                    UserDialogs.Instance.HideLoading();
                    await DialogService.ShowAlertAsync(WebControlTitle.GetTargetNameByTitleName("InvalidCredentials"), WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    return;
                }

                if (Application.Current.Properties.ContainsKey("SRID"))
                {
                     SRID = Application.Current.Properties["SRID"].ToString();

                }
                if (Application.Current.Properties.ContainsKey("ServiceRequestNavigation"))
                {
                    SRNavigation =Application.Current.Properties["ServiceRequestNavigation"].ToString();

                }

                
                var yourobject = new ServiceRequestWrapper
                {
                    TimeZone = "UTC",
                    CultureName = "en-US",
                    UserId = Convert.ToInt32(AppSettings.User.UserID),
                    serviceRequest = new ServiceRequests
                    {
                        IsSignatureValidated = true,
                        ModifiedUserName=AppSettings.User.UserName,
                        ServiceRequestID =Convert.ToInt32(SRID),

                    },

                };


                var response = await ServiceRequestService.AcceptServiceRequest(yourobject);

                if (Boolean.Parse(response.servicestatus))
                {
                    Application.Current.Properties["UserNameType"] = "Label";
                    SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                    if (SRNavigation=="True")
                    {
                        
                        await navigationService.NavigateBackAsync();
                    }
                    else
                    {
                       
                        await Navigation.PopAsync();
                        await navigationService.NavigateToAsync<ServiceRequestListingPageViewModel>();
                        
                    }
                    
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